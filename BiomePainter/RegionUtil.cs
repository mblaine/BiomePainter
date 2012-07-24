using System;
using System.Collections.Generic;
using System.Drawing;
using Minecraft;

namespace BiomePainter
{
    public class RegionUtil
    {
        internal const int OFFSETX = 32;
        internal const int OFFSETY = 32;
        private const int WIDTH = 512;
        private const int HEIGHT = 512;
        private static readonly Rectangle CLIP = new Rectangle(OFFSETX, OFFSETY, WIDTH, HEIGHT);

        private RegionUtil()
        {
        }

        private static void RenderSurroundingRegion(RegionFile region, int chunkStartX, int chunkEndX, int chunkStartZ, int chunkEndZ, int offsetX, int offsetY, Bitmap map, Bitmap biomes, String[,] toolTips, Bitmap populate)
        {
            using (Graphics g = Graphics.FromImage(populate))
            {
                Brush brush = new SolidBrush(Color.Yellow);
                for (int x = chunkStartX; x <= chunkEndX; x++)
                {
                    for (int z = chunkStartZ; z <= chunkEndZ; z++)
                    {
                        Chunk c = region.Chunks[x, z];
                        if (c == null || c.Root == null)
                            continue;
                        Coord chunkOffset = new Coord(c.Coords);
                        chunkOffset.ChunktoRegionRelative();
                        chunkOffset = new Coord(chunkOffset.X - chunkStartX, chunkOffset.Z - chunkStartZ);
                        chunkOffset.ChunktoAbsolute();
                        chunkOffset.Add(offsetX, offsetY);

                        RenderChunk(c, map, chunkOffset.X, chunkOffset.Z);
                        RenderChunkBiomes(c, biomes, toolTips, chunkOffset.X, chunkOffset.Z);
                        RenderChunktobePopulated(c, g, brush, chunkOffset.X, chunkOffset.Z);
                    }
                }
                brush.Dispose();
            }
        }

        public static void RenderSurroundingRegions(RegionFile[,] regions, Bitmap map, Bitmap biomes, String[,] toolTips, Bitmap populate)
        {
            RenderSurroundingRegion(regions[0, 0], 30, 31, 30, 31, 0, 0, map, biomes, toolTips, populate);
            RenderSurroundingRegion(regions[1, 0], 0, 31, 30, 31, OFFSETX, 0, map, biomes, toolTips, populate);
            RenderSurroundingRegion(regions[2, 0], 0, 1, 30, 31, OFFSETX + WIDTH, 0, map, biomes, toolTips, populate);
            RenderSurroundingRegion(regions[0, 1], 30, 31, 0, 31, 0, OFFSETY, map, biomes, toolTips, populate);

            RenderSurroundingRegion(regions[2, 1], 0, 1, 0, 31, OFFSETX + WIDTH, OFFSETY, map, biomes, toolTips, populate);
            RenderSurroundingRegion(regions[0, 2], 30, 31, 0, 1, 0, OFFSETY + HEIGHT, map, biomes, toolTips, populate);
            RenderSurroundingRegion(regions[1, 2], 0, 31, 0, 1, OFFSETX, OFFSETY + HEIGHT, map, biomes, toolTips, populate);
            RenderSurroundingRegion(regions[2, 2], 0, 1, 0, 1, OFFSETX + WIDTH, OFFSETY + HEIGHT, map, biomes, toolTips, populate);

        }

        private static void RenderChunk(Chunk c, Bitmap b, int offsetX, int offsetY)
        {
            TAG_Compound[] sections = new TAG_Compound[16];
            int highest = -1;
            foreach (TAG t in (TAG[])c.Root["Level"]["Sections"])
            {
                byte index = (byte)t["Y"];
                if (index > highest)
                    highest = index;
                sections[index] = (TAG_Compound)t;

            }

            if (c.ManualHeightmap == null)
            {
                c.ManualHeightmap = new int[256];
                for (int i = 0; i < c.ManualHeightmap.Length; i++)
                    c.ManualHeightmap[i] = -1;
            }

            //chunk exists but all blocks are air
            if (highest < 0)
                return;

            highest = ((highest + 1) * 16) - 1;

            TAG biomes = null;
            c.Root["Level"].TryGetValue("Biomes", out biomes);

            for (int z = 0; z < 16; z++)
            {
                for (int x = 0; x < 16; x++)
                {
                    int y;
                    if (c.ManualHeightmap[x + z * 16] >= 0)
                        y = c.ManualHeightmap[x + z * 16];
                    else
                    {
                        y = GetHeight(sections, x, z, highest);
                        c.ManualHeightmap[x + z * 16] = y;
                    }
                    
                    if (y < 0)
                        continue;
                    byte id, data;
                    GetBlock(sections, x, y, z, out id, out data);
                    byte biome = (byte)Biome.Unspecified;
                    if(biomes != null && Settings.BiomeFoliage)
                        biome = ((byte[])biomes)[x + z * 16];

                    Color color = ColorPalette.Lookup(id, data, biome);

                    if (Settings.Transparency)
                    {
                        y--;
                        while (color.A < 255 && y >= 0)
                        {
                            GetBlock(sections, x, y, z, out id, out data);
                            Color c2 = ColorPalette.Lookup(id, data, biome);
                            color = Blend(color, c2);
                            y--;
                        }
                    }
                    else
                        color = Color.FromArgb(255, color.R, color.G, color.B);

                    //brighten/darken by height; arbitrary value, but /seems/ to look okay
                    color = AddtoColor(color, (int)(y / 1.7 - 42));
                    
                    b.SetPixel(offsetX + x, offsetY + z, color);
                }
            }
        }

        public static void RenderRegion(RegionFile region, Bitmap b, bool clip = true)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                if(clip)
                    g.SetClip(CLIP);
                g.Clear(Color.Black);
            }

            foreach (Chunk c in region.Chunks)
            {
                if (c == null || c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(c.Coords);
                chunkOffset.ChunktoRegionRelative();
                chunkOffset.ChunktoAbsolute();
                RenderChunk(c, b, OFFSETX + chunkOffset.X, OFFSETY + chunkOffset.Z);
            }
        }

        private static void RenderChunkBiomes(Chunk c, Bitmap b, String[,] toolTips, int offsetX, int offsetY)
        {
            TAG t = null;
            if (c.Root["Level"].TryGetValue("Biomes", out t))
            {
                byte[] biomes = (byte[])t;

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        byte biome = biomes[x + z * 16];
                        Color color;
                        if (BiomeType.Biomes[biome] != null)
                        {
                            color = BiomeType.Biomes[biome].Color;
                            toolTips[offsetX + x, offsetY + z] = BiomeType.Biomes[biome].Name;
                        }
                        else
                        {
                            color = Color.Black;
                            toolTips[offsetX + x, offsetY + z] = String.Format("Unknown biome: {0}", biome);
                        }
                        b.SetPixel(offsetX + x, offsetY + z, color);

                    }
                }
            }
            else
            {
                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        toolTips[offsetX + x, offsetY + z] = BiomeType.Biomes[(int)Biome.Unspecified].Name;
                        b.SetPixel(offsetX + x, offsetY + z, BiomeType.Biomes[(int)Biome.Unspecified].Color);
                    }
                }
            }
        }

        public static void RenderRegionBiomes(RegionFile region, Bitmap b, String[,] toolTips, bool clip = true)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                if(clip)
                    g.SetClip(CLIP);
                g.Clear(Color.Black);
            }

            foreach (Chunk c in region.Chunks)
            {
                if (c == null || c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(c.Coords);
                chunkOffset.ChunktoRegionRelative();
                chunkOffset.ChunktoAbsolute();
                RenderChunkBiomes(c, b, toolTips, OFFSETX + chunkOffset.X, OFFSETY + chunkOffset.Z);
            }
        }

        public static void RenderChunktobePopulated(Chunk c, Graphics g, Brush brush, int offsetX, int offsetY)
        {
            if (((byte)c.Root["Level"]["TerrainPopulated"]) == 0)
            {
                g.FillRectangle(brush, offsetX, offsetY, 16, 16);
            }
        }

        public static void RenderRegionChunkstobePopulated(RegionFile region, Bitmap b, bool clip = true)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                if(clip)
                    g.SetClip(CLIP);
                g.Clear(Color.Transparent);

                Brush brush = new SolidBrush(Color.Yellow);
                for (int chunkX = 0; chunkX < 32; chunkX++)
                {
                    for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                    {
                        Chunk c = region.Chunks[chunkX, chunkZ];
                        if (c == null || c.Root == null)
                            continue;
                        RenderChunktobePopulated(c, g, brush, OFFSETX + chunkX * 16, OFFSETY + chunkZ * 16);
                    }
                }
                brush.Dispose();
            }
        }

        private static int GetHeight(TAG_Compound[] sections, int x, int z, int yStart = 255)
        {
            int h = yStart;
            for (; h >= 0; h--)
            {
                byte b = GetBlock(sections, x, h, z);
                if (b != 0)
                    return h;
            }
            return -1;
        }

        private static byte GetBlock(TAG_Compound[] sections, int x, int y, int z)
        {
            byte id, data;
            GetBlock(sections, x, y, z, out id, out data);
            return id;
        }

        private static void GetBlock(TAG_Compound[] sections, int x, int y, int z, out byte id, out byte data)
        {
            id = 0;
            data = 0;
            int section = (int)Math.Floor(y / 16.0);

            if (sections[section] != null)
            {
                int offset = ((y % 16) * 16 + z) * 16 + x;
                id = ((byte[])sections[section]["Blocks"])[offset];
                data = ((byte[])sections[section]["Data"])[offset >> 1];
                if (offset % 2 == 0)
                    data = (byte)(data & 0x0F);
                else
                    data = (byte)((data >> 4) & 0x0F);
            }
        }

        private static Color AddtoColor(Color c, int diff)
        {
            int red = c.R + diff;
            if (red > 255)
                red = 255;
            else if (red < 0)
                red = 0;
            int green = c.G + diff;
            if (green > 255)
                green = 255;
            else if (green < 0)
                green = 0;
            int blue = c.B + diff;
            if (blue > 255)
                blue = 255;
            else if (blue < 0)
                blue = 0;
            return Color.FromArgb(c.A, red, green, blue);
        }

        private static Color Blend(Color c1, Color c2)
        {
            if (c2.A == 0)
                return c1;
            else if (c1.A == 0)
                return c2;

            double a1 = c1.A / 255.0;
            double a2 = c2.A / 255.0;
            a2 *= (1.0 - a1);
            double a = a1 + a2;

            int r = (int)(c1.R * a1 + c2.R * a2);
            int g = (int)(c1.G * a1 + c2.G * a2);
            int b = (int)(c1.B * a1 + c2.B * a2);
            a *= 255;

            if (c1.A == 255 || c2.A == 255)
                a = 255;
            return Color.FromArgb((int)a, r, g, b);
        }

        private static void GetBiome(Object input, long seed, out byte? biomeId, out BiomeUtil biomeGen)
        {
            biomeId = null;
            biomeGen = null;

            if (input is BiomeType)
            {
                biomeId = ((BiomeType)input).ID;
            }
            else if (input is String)
            {
                switch ((String)input)
                {
                    case "Minecraft Beta 1.3_01":
                        biomeGen = new Minecraft.B13.MobSpawnerBase(seed);
                        break;
                    case "Minecraft Beta 1.7.3":
                        biomeGen = new Minecraft.B17.BiomeGenBase(seed);
                        break;
                    case "Minecraft Beta 1.8.1":
                        biomeGen = new Minecraft.B18.WorldChunkManager(seed);
                        break;
                    case "Minecraft 1.0.0":
                        biomeGen = new Minecraft.F10.WorldChunkManager(seed);
                        break;
                    case "Minecraft 1.1.0":
                        biomeGen = new Minecraft.F11.WorldChunkManager(seed);
                        break;
                    case "Minecraft 1.2.5":
                    default:
                        biomeGen = new Minecraft.F12.WorldChunkManager(seed);
                        break;
                }
            }
        }

        public static void Fill(RegionFile region, Bitmap selection, Color selectionColor, Object biome, long worldSeed)
        {
            byte? biomeId;
            BiomeUtil util;
            GetBiome(biome, worldSeed, out biomeId, out util);
            if (biomeId != null)
                Fill(region, selection, selectionColor, (byte)biomeId);
            else
                Fill(region, selection, selectionColor, util);
        }

        private static void Fill(RegionFile region, Bitmap selection, Color selectionColor, byte biome)
        {
            foreach (Chunk c in region.Chunks)
            {
                if (c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(region.Coords);
                chunkOffset.RegiontoChunk();
                chunkOffset = new Coord(c.Coords.X - chunkOffset.X, c.Coords.Z - chunkOffset.Z);
                chunkOffset.ChunktoAbsolute();

                byte[] biomes = (byte[])c.Root["Level"]["Biomes"];
                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if (selection == null || (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb()))
                        {
                            biomes[x + z * 16] = biome;
                        }
                    }
                }
            }
        }

        private static void Fill(RegionFile region, Bitmap selection, Color selectionColor, BiomeUtil util)
        {
            foreach (Chunk c in region.Chunks)
            {
                if (c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(region.Coords);
                chunkOffset.RegiontoChunk();
                chunkOffset = new Coord(c.Coords.X - chunkOffset.X, c.Coords.Z - chunkOffset.Z);
                chunkOffset.ChunktoAbsolute();

                Coord chunkAbs = new Coord(c.Coords);
                chunkAbs.ChunktoAbsolute();

                byte[] biomes = (byte[])c.Root["Level"]["Biomes"];

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if (selection == null || (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb()))
                        {
                            biomes[x + z * 16] = (byte)util.GetBiome(chunkAbs.X + x, chunkAbs.Z + z);
                        }
                    }
                }
            }
        }

        public static void Replace(RegionFile region, Bitmap selection, Color selectionColor, byte biome1, Object biome2, long worldSeed)
        {
            byte? biomeId;
            BiomeUtil util;
            GetBiome(biome2, worldSeed, out biomeId, out util);
            if (biomeId != null)
                Replace(region, selection, selectionColor, biome1, (byte)biomeId);
            else
                Replace(region, selection, selectionColor, biome1, util);
        }

        private static void Replace(RegionFile region, Bitmap selection, Color selectionColor, byte search, byte replace)
        {
            foreach (Chunk c in region.Chunks)
            {
                if (c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(region.Coords);
                chunkOffset.RegiontoChunk();
                chunkOffset = new Coord(c.Coords.X - chunkOffset.X, c.Coords.Z - chunkOffset.Z);
                chunkOffset.ChunktoAbsolute();

                byte[] biomes = (byte[])c.Root["Level"]["Biomes"];

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if (selection == null || (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb()))
                        {
                            if (biomes[x + z * 16] == search)
                            {
                                biomes[x + z * 16] = replace;
                            }
                        }
                    }
                }
            }
        }

        private static void Replace(RegionFile region, Bitmap selection, Color selectionColor, byte search, BiomeUtil replace)
        {
            foreach (Chunk c in region.Chunks)
            {
                if (c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(region.Coords);
                chunkOffset.RegiontoChunk();
                chunkOffset = new Coord(c.Coords.X - chunkOffset.X, c.Coords.Z - chunkOffset.Z);
                chunkOffset.ChunktoAbsolute();

                Coord chunkAbs = new Coord(c.Coords);
                chunkAbs.ChunktoAbsolute();

                byte[] biomes = (byte[])c.Root["Level"]["Biomes"];

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if (selection == null || (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb()))
                        {
                            if (biomes[x + z * 16] == search)
                            {
                                biomes[x + z * 16] = (byte)replace.GetBiome(chunkAbs.X + x, chunkAbs.Z + z);
                            }
                        }
                    }
                }
            }
        }

        public static void SetChunkstobePopulated(RegionFile region, Bitmap selection, Color selectionColor, byte value)
        {
            for (int chunkX = 0; chunkX < 32; chunkX++)
            {
                for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                {
                    Chunk c = region.Chunks[chunkX, chunkZ];
                    if (c == null || c.Root == null)
                        continue;

                    bool done = false;
                    for (int z = 0; z < 16; z++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            if (selection.GetPixel(OFFSETX + chunkX * 16 + x, OFFSETY + chunkZ * 16 + z).ToArgb() == selectionColor.ToArgb())
                            {
                                ((TAG_Byte)c.Root["Level"]["TerrainPopulated"]).Payload = value;
                                done = true;
                                break;
                            }
                        }
                        if (done)
                            break;
                    }
                }
            }
        }

        public static void SelectChunks(Bitmap b, Color selectionColor)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.SetClip(CLIP);
                Brush brush = new SolidBrush(selectionColor);
                for (int chunkX = 0; chunkX < 32; chunkX++)
                {
                    for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                    {
                        bool shouldSelect = false;
                        for (int x = 0; x < 16; x++)
                        {
                            for (int z = 0; z < 16; z++)
                            {
                                if (b.GetPixel(OFFSETX + chunkX * 16 + x, OFFSETY + chunkZ * 16 + z).ToArgb() == selectionColor.ToArgb())
                                {
                                    shouldSelect = true;
                                    break;
                                }
                            }
                            if (shouldSelect)
                                break;
                        }

                        if (shouldSelect)
                            g.FillRectangle(brush, OFFSETX + chunkX * 16, OFFSETY + chunkZ * 16, 16, 16);
                    }
                }
            }
        }

        public static void AddorRemoveBlocksSelection(RegionFile region, Bitmap b, Color selectionColor, int[] blockIds, bool add)
        {
            if (blockIds == null || blockIds.Length == 0)
                return;

            List<int> ids = new List<int>(blockIds);

            foreach (Chunk c in region.Chunks)
            {
                if (c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(region.Coords);
                chunkOffset.RegiontoChunk();
                chunkOffset = new Coord(c.Coords.X - chunkOffset.X, c.Coords.Z - chunkOffset.Z);
                chunkOffset.ChunktoAbsolute();

                TAG_Compound[] sections = new TAG_Compound[16];
                int highest = -1;
                foreach (TAG t in (TAG[])c.Root["Level"]["Sections"])
                {
                    byte index = (byte)t["Y"];
                    if (index > highest)
                        highest = index;
                    sections[index] = (TAG_Compound)t;
                }

                //chunk exists but all blocks are air
                if (highest < 0)
                    return;

                highest = ((highest + 1) * 16) - 1;

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int y;
                        if (c.ManualHeightmap[x + z * 16] >= 0)
                            y = c.ManualHeightmap[x + z * 16];
                        else
                        {
                            y = GetHeight(sections, x, z, highest);
                            c.ManualHeightmap[x + z * 16] = y;
                        }
                        if (y < 0)
                            continue;

                        if (ids.Contains(GetBlock(sections, x, y, z)))
                        {
                            b.SetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z, add ? selectionColor : Color.Transparent);
                        }
                    }
                }
            }
        }

        public static void AddorRemoveBiomesSelection(RegionFile region, Bitmap b, Color selectionColor, byte biome, bool add)
        {
            foreach (Chunk c in region.Chunks)
            {
                if (c.Root == null)
                    continue;
                Coord chunkOffset = new Coord(region.Coords);
                chunkOffset.RegiontoChunk();
                chunkOffset = new Coord(c.Coords.X - chunkOffset.X, c.Coords.Z - chunkOffset.Z);
                chunkOffset.ChunktoAbsolute();

                byte[] biomes = (byte[])c.Root["Level"]["Biomes"];

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if(biome == biomes[x + z * 16])
                            b.SetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z, add ? selectionColor : Color.Transparent);
                    }
                }
            }

        }

        public static void RenderChunkBoundaries(Bitmap b)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.Black);
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                Brush brushFill = new SolidBrush(Color.SlateGray);
                Brush brushClear = new SolidBrush(Color.Transparent);
                for (int chunkX = 0; chunkX < 32; chunkX++)
                {
                    for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                    {
                        if ((chunkZ % 2 == 0 && chunkX % 2 == 1) || (chunkZ % 2 == 1 && chunkX % 2 == 0))
                            g.FillRectangle(brushFill, OFFSETX + chunkX * 16, OFFSETY + chunkZ * 16, 16, 16);
                        else
                            g.FillRectangle(brushClear, OFFSETX + chunkX * 16, OFFSETY + chunkZ * 16, 16, 16);

                    }
                }
                brushFill.Dispose();
                brushClear.Dispose();
            }
        }
    }
}