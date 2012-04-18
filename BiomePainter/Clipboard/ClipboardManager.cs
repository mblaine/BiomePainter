using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Minecraft;

namespace BiomePainter.Clipboard
{
    public class ClipboardManager
    {
        private static BiomeCopy currentPaste = null;

        private ClipboardManager()
        {
        }

        public static void Copy(RegionFile region, Bitmap selection, Color selectionColor)
        {
            BiomeCopy biomeData = new BiomeCopy();
            for (int chunkX = 0; chunkX < 32; chunkX++)
            {
                for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    Chunk c = region.Chunks[chunkX, chunkZ];
                    if (c == null || c.Root == null)
                        continue;

                    byte[] biomes = (byte[])c.Root["Level"]["Biomes"];
                    for (int x = 0; x < 16; x++)
                    {
                        for (int z = 0; z < 16; z++)
                        {
                            if (selection.GetPixel(RegionUtil.OFFSETX + chunkX * 16 + x, RegionUtil.OFFSETY + chunkZ * 16 + z).ToArgb() == selectionColor.ToArgb())
                            {
                                biomeData.Biomes[chunkX * 16 + x, chunkZ * 16 + z] = biomes[x + z * 16];
                                biomeData.Empty = false;
                                if (biomeData.Left > chunkX * 16 + x)
                                    biomeData.Left = chunkX * 16 + x;
                                if (biomeData.Right < chunkX * 16 + x)
                                    biomeData.Right = chunkX * 16 + x;
                                if (biomeData.Top > chunkZ * 16 + z)
                                    biomeData.Top = chunkZ * 16 + z;
                                if (biomeData.Bottom < chunkZ * 16 + z)
                                    biomeData.Bottom = chunkZ * 16 + z;
                            }
                        }
                    }
                }
            }
            biomeData.Crop();
            System.Windows.Forms.Clipboard.SetData("BiomeCopy", biomeData);
        }

        public static Bitmap StartPaste()
        {
            currentPaste = (BiomeCopy)System.Windows.Forms.Clipboard.GetData("BiomeCopy");
            if (currentPaste == null)
                return null;
            if (currentPaste.Empty)
            {
                currentPaste = null;
                return null;
            }

            return currentPaste.ToBitmap();
        }

        public static bool Paste(RegionFile region, int offsetX, int offsetZ)
        {
            if (currentPaste == null)
                return false;

            for (int x = 0; x < currentPaste.Width; x++)
            {
                for (int z = 0; z < currentPaste.Height; z++)
                {
                    if (currentPaste.Biomes[x, z] == (byte)Biome.Unspecified)
                        continue;

                    Coord chunkOffset = new Coord(offsetX + x, offsetZ + z);
                    chunkOffset.AbsolutetoChunk();
                    if (chunkOffset.X < 0 || chunkOffset.X >= 32 || chunkOffset.Z < 0 || chunkOffset.Z >= 32)
                        continue;
                    Chunk c = region.Chunks[chunkOffset.X, chunkOffset.Z];
                    if (c == null || c.Root == null)
                        continue;
                    
                    int pasteX = (offsetX + x) % 16;
                    int pasteZ = (offsetZ + z) % 16;

                    ((TAG_Byte_Array)c.Root["Level"]["Biomes"]).Payload[pasteX + pasteZ * 16] = currentPaste.Biomes[x, z];
                }
            }

            return true;
        }
    }
}
