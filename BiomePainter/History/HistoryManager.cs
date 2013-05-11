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

        public delegate void ChangeCallback(String undoDescription, String redoDescription);

        private ChangeCallback change;

        private LinkedList<IAction> undoStack = new LinkedList<IAction>();
        private LinkedList<IAction> redoStack = new LinkedList<IAction>();

        //the actions at the last point the region was saved
        private BiomeAction lastBiomeAction;
        private PopulateAction lastPopulateAction;


        //the original state of things
        private SelectionAction firstSelectionAction = null;
        private BiomeAction firstBiomeAction = null;
        private PopulateAction firstPopulateAction = null;


        public HistoryManager(ChangeCallback change)
        {
            this.change = change;
        }

        public void Dispose()
        {
            if (undoStack != null)
                foreach (IAction action in undoStack)
                    action.Dispose();
            undoStack = null;

            if (redoStack != null)
                foreach (IAction action in redoStack)
                    action.Dispose();
            redoStack = null;
        }

        public void Add(IAction action)
        {
            if(action.GetType() == typeof(SelectionAction) && firstSelectionAction == null)
                firstSelectionAction = (SelectionAction)action;
            else if (action.GetType() == typeof(BiomeAction) && firstBiomeAction == null)
                firstBiomeAction = (BiomeAction)action;
            else if (action.GetType() == typeof(PopulateAction) && firstPopulateAction == null)
                firstPopulateAction = (PopulateAction)action;
            
            //ensure the first action of each type isn't lost when the redo stack is emptied
            if(firstSelectionAction != null && action != firstSelectionAction && redoStack.Contains(firstSelectionAction))
            {
                redoStack.Remove(firstSelectionAction);
                undoStack.AddLast(firstSelectionAction);
            }
            if (firstBiomeAction != null && action != firstBiomeAction && redoStack.Contains(firstBiomeAction))
            {
                redoStack.Remove(firstBiomeAction);
                undoStack.AddLast(firstBiomeAction);
            }
            if (firstPopulateAction != null && action != firstPopulateAction && redoStack.Contains(firstPopulateAction))
            {
                redoStack.Remove(firstPopulateAction);
                undoStack.AddLast(firstPopulateAction);
            }

            action.PreviousAction = GetPreviousAction(undoStack.Last, action.GetType());
            undoStack.AddLast(action);
            foreach (IAction a in redoStack)
                a.Dispose();
            redoStack.Clear();
        }

        public void FilterOutType(Type type)
        {
            LinkedList<IAction> newUndo = new LinkedList<IAction>();

            foreach (IAction action in undoStack)
            {
                if (type.Equals(action.GetType()))
                    action.Dispose();
                else
                    newUndo.AddLast(action);
            }

            undoStack = newUndo;

            LinkedList<IAction> newRedo = new LinkedList<IAction>();

            foreach (IAction action in redoStack)
            {
                if (type.Equals(action.GetType()))
                    action.Dispose();
                else
                    newRedo.AddLast(action);
            }

            if (firstBiomeAction != null && type == typeof(BiomeAction))
                firstBiomeAction = null;
            if (firstPopulateAction != null && type == typeof(PopulateAction))
                firstPopulateAction = null;
            if (firstSelectionAction != null && type == typeof(SelectionAction))
                firstSelectionAction = null;

            redoStack = newRedo;
        }

        public bool MovePrevious()
        {
            if (undoStack.Count == 0)
                return false;

            do
            {
                redoStack.AddLast(undoStack.Last.Value);
                undoStack.RemoveLast();
            }
            while (undoStack.Count > 0 && undoStack.Last.Value.PreviousAction == null);

            return undoStack.Count > 0;
        }

        private bool MoveNext()
        {
            if (redoStack.Count == 0)
                return false;

            do
            {
                undoStack.AddLast(redoStack.Last.Value);
                redoStack.RemoveLast();
            }
            while (redoStack.Count > 0 && undoStack.Last.Value.PreviousAction == null);

            return true;
        }

        public void RecordSelectionState(Bitmap selection, String description)
        {
            Add(new SelectionAction(new Bitmap(selection), description));
            OnChange();
        }

        public void RecordBiomeState(RegionFile region, String description)
        {
            BiomeAction action = new BiomeAction(description);
            for (int chunkX = 0; chunkX < 32; chunkX++)
            {
                for(int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    Chunk c = region.Chunks[chunkX, chunkZ];
                    if (c == null || c.Root == null)
                        continue;

                    //first point of accessing chunk's biomes, make sure it exists
                    TAG_Compound level = (TAG_Compound)c.Root["Level"];
                    byte[] biomes;
                    if (level.Payload.ContainsKey("Biomes"))
                        biomes = (byte[])level["Biomes"];
                    else
                    {
                        biomes = new byte[256];
                        for (int i = 0; i < biomes.Length; i++)
                            biomes[i] = (byte)Biome.Unspecified;
                        level.Payload.Add("Biomes", new TAG_Byte_Array(biomes, "Biomes"));
                    }

                    action.Chunks.Add(new ChunkState(chunkX, chunkZ, (byte[])biomes.Clone()));
                }
            }
            Add(action);
            OnChange();
        }

        public void RecordPopulateState(RegionFile region, String description)
        {
            PopulateAction action = new PopulateAction(description);
            for (int chunkX = 0; chunkX < 32; chunkX++)
            {
                for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    Chunk c = region.Chunks[chunkX, chunkZ];
                    if (c == null || c.Root == null)
                        continue;

                    action.PopulatedFlags[chunkX, chunkZ] = (byte)c.Root["Level"]["TerrainPopulated"];
                }
            }
            Add(action);
            OnChange();
        }

        private void ApplySelectionState(SelectionAction action, Bitmap selection)
        {
            using (Graphics g = Graphics.FromImage(selection))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawImage(action.Image, 0, 0);
            }
        }

        private void ApplyBiomeState(BiomeAction action, RegionFile region, Bitmap terrainOverlay, Bitmap biomeOverlay, ref String[,] tooltips, UpdateStatus updateStatus)
        {
            foreach (ChunkState state in action.Chunks)
            {
                Chunk c = region.Chunks[state.Coords.X, state.Coords.Z];
                if (c == null || c.Root == null)
                    continue;
                ((TAG_Byte_Array)c.Root["Level"]["Biomes"]).Payload = (byte[])state.Biomes.Clone();
            }
            tooltips = new String[biomeOverlay.Width, biomeOverlay.Height];
            if (terrainOverlay != null)
            {
                updateStatus("Generating terrain map");
                RegionUtil.RenderRegionTerrain(region, terrainOverlay);
            }
            updateStatus("Generating biome map");
            RegionUtil.RenderRegionBiomes(region, biomeOverlay, tooltips);
            updateStatus("");
        }

        private void ApplyPopulateState(PopulateAction action, RegionFile region, Bitmap populateOverlay)
        {
            for (int chunkX = 0; chunkX < 32; chunkX++)
            {
                for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    Chunk c = region.Chunks[chunkX, chunkZ];
                    if (c == null || c.Root == null)
                        continue;

                    ((TAG_Byte)c.Root["Level"]["TerrainPopulated"]).Payload = action.PopulatedFlags[chunkX, chunkZ];
                }
            }
            RegionUtil.RenderRegionChunkstobePopulated(region, populateOverlay);
        }

        public void Undo(Bitmap selection, RegionFile region, Bitmap terrainOverlay, Bitmap biomeOverlay, ref String[,] tooltips, Bitmap populateOverlay, UpdateStatus updateStatus)
        {
            while (undoStack.Count > 0 && undoStack.Last.Value.PreviousAction == null)
            {
                redoStack.AddLast(undoStack.Last.Value);
                undoStack.RemoveLast();
            }

            if (undoStack.Count == 0)
                return;

            IAction previous = undoStack.Last.Value.PreviousAction;
            if (previous == null)
            {
                throw new Exception("Undo sanity check failed.");
            }

            if (previous is SelectionAction)
            {
                ApplySelectionState((SelectionAction)previous, selection);
            }
            else if (previous is BiomeAction)
            {
                ApplyBiomeState((BiomeAction)previous, region, terrainOverlay, biomeOverlay, ref tooltips, updateStatus);
            }
            else if (previous is PopulateAction)
            {
                ApplyPopulateState((PopulateAction)previous, region, populateOverlay); 
            }

            MovePrevious();
            OnChange();
        }

        public void Redo(Bitmap selection, RegionFile region, Bitmap terrainOverlay, Bitmap biomeOverlay, ref String[,] tooltips, Bitmap populateOverlay, UpdateStatus updateStatus)
        {
            if (!MoveNext())
                return;

            if (undoStack.Last.Value.PreviousAction == null)
            {
                throw new Exception("Redo sanity check failed.");
            }

            if (undoStack.Last.Value is SelectionAction)
            {
                ApplySelectionState((SelectionAction)undoStack.Last.Value, selection);
            }
            else if (undoStack.Last.Value is BiomeAction)
            {
                ApplyBiomeState((BiomeAction)undoStack.Last.Value, region, terrainOverlay, biomeOverlay, ref tooltips, updateStatus);
            }
            else if (undoStack.Last.Value is PopulateAction)
            {
                ApplyPopulateState((PopulateAction)undoStack.Last.Value, region, populateOverlay);
            }

            OnChange();
        }

        private IAction GetPreviousAction()
        {
            if (undoStack.Count == 0)
                return null;
            return GetPreviousAction(undoStack.Last.Previous, undoStack.Last.Value.GetType());
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

        public void SetLastSaveActions()
        {
            lastBiomeAction = (BiomeAction)GetPreviousAction(undoStack.Last, typeof(BiomeAction));
            lastPopulateAction = (PopulateAction)GetPreviousAction(undoStack.Last, typeof(PopulateAction));
        }

        public void SetDirtyFlags(RegionFile region)
        {
            if (lastBiomeAction == null || lastPopulateAction == null)
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

            for (int chunkX = 0; chunkX < 32; chunkX++)
            {
                for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    Chunk c = region.Chunks[chunkX, chunkZ];
                    if (c == null)
                        continue;
                    else if (c.Root == null)
                    {
                        c.Dirty = false;
                        continue;
                    }

                    if (((byte)c.Root["Level"]["TerrainPopulated"]) != lastPopulateAction.PopulatedFlags[chunkX, chunkZ])
                    {
                        c.Dirty = true;
                        region.Dirty = true;
                    }
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

        private void OnChange()
        {
            if (change != null)
            {
                //the top action on a stack may be the first of its kind and therefore
                //needs to be skipped over; the first action of a kind is only needed
                //when undoing the second action of that kind, via it's PreviousAction
                //property
                String redoDescription = null;
                if(redoStack.Count > 0)
                {
                    LinkedListNode<IAction> redo = redoStack.Last;
                    while (redo != null && redo.Value.PreviousAction == null)
                        redo = redo.Previous;
                    if (redo != null)
                        redoDescription = redo.Value.Description;
                }
                String undoDescription = null;
                if (undoStack.Count > 0)
                {
                    LinkedListNode<IAction> undo = undoStack.Last;
                    while (undo != null && undo.Value.PreviousAction == null)
                        undo = undo.Previous;
                    if (undo != null)
                        undoDescription = undo.Value.Description;
                }
                change(undoDescription, redoDescription);
            }
        }
    }
}
