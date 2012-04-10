using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Minecraft;

namespace BiomePainter.Clipboard
{
    public class ClipboardManager
    {
        private static BiomeCopy biomeData = null;

        private ClipboardManager()
        {
        }

        public static void Copy(RegionFile region, Bitmap selection, Color selectionColor)
        {
            biomeData = new BiomeCopy();
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
        }

        //return true if anything was altered and needs redrawing
        public static bool Paste(RegionFile region)
        {
            if (biomeData == null || biomeData.Empty)
                return false;

            String msg = "Type the x (horizontal) and z (vertical) coordinates to paste at, between 0, 0 (top-left corner) and 511, 511 (bottom-right corner).";
            String input = String.Format("{0}, {1}", biomeData.Left, biomeData.Top);
            while (true)
            {
                input = InputBox.Show(msg, "Paste", input);
                if (input.Length == 0)
                    return false;

                Match m = Regex.Match(input, @"([-\+]?\d+)(?:[,\s]+)([-\+]?\d+)");
                if (m.Groups.Count < 3)
                {
                    msg = "Unable to parse coordinates. Please try again or click cancel.";
                }
                else
                {
                    int x, z;
                    if (!Int32.TryParse(m.Groups[1].Value, out x) || !Int32.TryParse(m.Groups[2].Value, out z))
                    {
                        msg = "Unable to parse coordinates. Please try again or click cancel.";
                    }
                    else
                    {
                        Paste(region, x, z);
                        return true;
                    }
                }
            }
        }

        private static void Paste(RegionFile region, int offsetX, int offsetZ)
        {
            for (int x = 0; x < biomeData.Width; x++)
            {
                for (int z = 0; z < biomeData.Height; z++)
                {
                    if (biomeData.Biomes[x, z] == (byte)Biome.Unspecified)
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

                    ((TAG_Byte_Array)c.Root["Level"]["Biomes"]).Payload[pasteX + pasteZ * 16] = biomeData.Biomes[x, z];
                }
            }
        }
    }
}
