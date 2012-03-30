using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Minecraft;

namespace BiomePainter.History
{
    public class HistoryManager : IDisposable
    {
        public delegate void UpdateStatus(String status);

        private LinkedList<IAction> UndoStack = new LinkedList<IAction>();
        private LinkedList<IAction> RedoStack = new LinkedList<IAction>();

        private BiomeAction lastBiomeAction;

        ~HistoryManager()
        {
            if (UndoStack != null)
                foreach (IAction action in UndoStack)
                    action.Dispose();
            UndoStack = null;

            if (RedoStack != null)
                foreach (IAction action in RedoStack)
                    action.Dispose();
            RedoStack = null;
        }

        public void Dispose()
        {
            if (UndoStack != null)
                foreach (IAction action in UndoStack)
                    action.Dispose();
            UndoStack = null;

            if (RedoStack != null)
                foreach (IAction action in RedoStack)
                    action.Dispose();
            RedoStack = null;
        }

        public void Add(IAction action)
        {
            UndoStack.AddLast(action);
            foreach (IAction a in RedoStack)
                a.Dispose();
            RedoStack.Clear();
        }

        public void FilterOutType(Type type)
        {
            LinkedList<IAction> newUndo = new LinkedList<IAction>();

            foreach (IAction action in UndoStack)
            {
                if (type.Equals(action.GetType()))
                    action.Dispose();
                else
                    newUndo.AddLast(action);
            }

            UndoStack = newUndo;

            LinkedList<IAction> newRedo = new LinkedList<IAction>();

            foreach (IAction action in RedoStack)
            {
                if (type.Equals(action.GetType()))
                    action.Dispose();
                else
                    newRedo.AddLast(action);
            }

            RedoStack = newRedo;
        }

        public bool MovePrevious()
        {
            if (UndoStack.Count <= 1)
                return false;
            RedoStack.AddLast(UndoStack.Last.Value);
            UndoStack.RemoveLast();
            return true;
        }

        private bool MoveNext()
        {
            if (RedoStack.Count == 0)
                return false;
            UndoStack.AddLast(RedoStack.Last.Value);
            RedoStack.RemoveLast();
            return true;
        }

        public void RecordSelectionState(Bitmap selection)
        {
            Add(new SelectionAction(new Bitmap(selection)));
        }

        public void RecordBiomeState(RegionFile region)
        {
            BiomeAction action = new BiomeAction();
            for (int chunkX = 0; chunkX < 32; chunkX++)
            {
                for(int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    Chunk c = region.Chunks[chunkX, chunkZ];
                    if (c == null || c.Root == null)
                        continue;
                    action.Chunks.Add(new ChunkState(chunkX, chunkZ, (byte[])((byte[])c.Root["Level"]["Biomes"]).Clone()));
                }
            }
            Add(action);
        }

        private void ApplySelectionState(SelectionAction action, Bitmap selection)
        {
            using (Graphics g = Graphics.FromImage(selection))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawImage(action.Image, 0, 0);
            }
        }

        private void ApplyBiomeState(BiomeAction action, RegionFile region, Bitmap biomeOverlay, ref String[,] tooltips, UpdateStatus updateStatus)
        {
            foreach (ChunkState state in ((BiomeAction)action).Chunks)
            {
                Chunk c = region.Chunks[state.Coords.X, state.Coords.Z];
                if (c == null || c.Root == null)
                    continue;
                ((TAG_Byte_Array)c.Root["Level"]["Biomes"]).Payload = (byte[])state.Biomes.Clone();
            }
            tooltips = new String[biomeOverlay.Width, biomeOverlay.Height];
            updateStatus("Generating biome map");
            RegionUtil.RenderRegionBiomes(region, biomeOverlay, tooltips);
            updateStatus("");
        }

        public void Undo(Bitmap selection, RegionFile region, Bitmap biomeOverlay, ref String[,] tooltips, UpdateStatus updateStatus)
        {
            IAction previous = GetPreviousAction();
            if (previous == null)
            {
                MovePrevious();
                return;
            }

            if (previous is SelectionAction)
            {
                ApplySelectionState((SelectionAction)previous, selection);
            }
            else if (previous is BiomeAction)
            {
                ApplyBiomeState((BiomeAction)previous, region, biomeOverlay, ref tooltips, updateStatus);
            }

            MovePrevious();
        }

        public void Redo(Bitmap selection, RegionFile region, Bitmap biomeOverlay, ref String[,] tooltips, UpdateStatus updateStatus)
        {
            if (!MoveNext())
                return;

            //if the first action of it's type, there should be nothing to overwrite
            if (GetPreviousAction(UndoStack.Last.Previous, UndoStack.Last.Value.GetType()) == null)
                return;

            if (UndoStack.Last.Value is SelectionAction)
            {
                ApplySelectionState((SelectionAction)UndoStack.Last.Value, selection);
            }
            else if (UndoStack.Last.Value is BiomeAction)
            {
                ApplyBiomeState((BiomeAction)UndoStack.Last.Value, region, biomeOverlay, ref tooltips, updateStatus);
            }
        }

        private IAction GetPreviousAction()
        {
            if (UndoStack.Count == 0)
                return null;
            return GetPreviousAction(UndoStack.Last.Previous, UndoStack.Last.Value.GetType());
        }

        private IAction GetPreviousAction(LinkedListNode<IAction> start, Type t)
        {
            while (start != null)
            {
                if (t.Equals(start.Value.GetType()))
                    return start.Value;
                start = start.Previous;
            }

            return null;
        }

        public void SetLastBiomeAction()
        {
            lastBiomeAction = (BiomeAction)GetPreviousAction(UndoStack.Last, typeof(BiomeAction));
        }

        public void SetDirtyFlags(RegionFile region)
        {
            if (lastBiomeAction == null)
                throw new Exception("No record of last region save state.");

            region.Dirty = false;
            foreach (ChunkState state in lastBiomeAction.Chunks)
            {
                Chunk c = region.Chunks[state.Coords.X, state.Coords.Z];
                if (c == null)
                    continue;
                else if (c.Root == null)
                {
                    c.Dirty = false;
                    continue;
                }

                if (ByteArraysEqual((byte[])c.Root["Level"]["Biomes"], state.Biomes))
                {
                    c.Dirty = false;
                }
                else
                {
                    c.Dirty = true;
                    region.Dirty = true;
                }
            }
        }

        private bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null)
                return false;
            if (b1.Length != b2.Length)
                return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i])
                    return false;
            }
            return true;
        }
    }
}
