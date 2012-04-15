using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
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

        internal static BiomeType[] Biomes = new BiomeType[256];

        private RegionUtil()
        {
        }

        public static void LoadBiomes(String path)
        {
            if (File.Exists(path))
            {
                Regex pattern = new Regex(@"^([0-9]+),([^,]+),([0-9a-fA-F]{6})$");
                String[] lines = File.ReadAllLines(path);
                foreach (String line in lines)
                {
                    Match m = pattern.Match(line);
                    if (m.Groups.Count == 4)
                    {
                        byte id = byte.Parse(m.Groups[1].Value);
                        if (id >= 0 && id < 255)
                        {
                            Biomes[id] = new BiomeType(id, m.Groups[2].Value, Convert.ToInt32(m.Groups[3].Value, 16));
                        }
                    }
                }
            }

            Biomes[255] = new BiomeType(255, "Unspecified", 0x000000);
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
            int[] heightmap = (int[])c.Root["Level"]["HeightMap"];
            Dictionary<int, TAG_Compound> sections = new Dictionary<int, TAG_Compound>();
            foreach (TAG t in (TAG[])c.Root["Level"]["Sections"])
            {
                sections.Add((byte)t["Y"], (TAG_Compound)t);
            }

            //chunk exists but all blocks are air
            if (sections.Count == 0)
                return;

            for (int z = 0; z < 16; z++)
            {
                for (int x = 0; x < 16; x++)
                {
                    int height = heightmap[z * 16 + x];

                    //trees runnning into the old height limit in converted worlds
                    //seem to cause the heightmap entries for its columns to be -128;
                    if (height < 0)
                        height = 128;

                    int sectionIndex = (int)Math.Floor((height - 1) / 16.0);
                    int sectionAboveIndex = (int)Math.Floor(height / 16.0);

                    int block = -1, damage = -1;
                    if (sections.ContainsKey(sectionIndex))
                    {
                        byte[] blocks = (byte[])sections[sectionIndex]["Blocks"];
                        byte[] data = (byte[])sections[sectionIndex]["Data"];
                        int blockOffset = ((((height - 1) % 16) * 16 + z) * 16 + x);
                        block = blocks[blockOffset];
                        damage = data[(int)Math.Floor(blockOffset / 2.0)];
                        if (blockOffset % 2 == 1)
                            damage = (damage >> 4) & 0x0F;
                        else
                            damage = damage & 0x0F;
                    }

                    int blockAbove = block;
                    if (sections.ContainsKey(sectionAboveIndex))
                    {
                        int blockAboveOffset = (((height % 16) * 16 + z) * 16 + x);
                        byte[] blocksAbove = (byte[])sections[sectionAboveIndex]["Blocks"];
                        blockAbove = blocksAbove[blockAboveOffset];
                    }

                    Color color = ColorLookup(block, damage, blockAbove);

                    //brighten/darken by height; arbitrary value, but /seems/ to look okay
                    color = AddtoColor(color, (int)(height / 1.7 - 42));

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
            byte[] biomes = (byte[])c.Root["Level"]["Biomes"];

            for (int z = 0; z < 16; z++)
            {
                for (int x = 0; x < 16; x++)
                {
                    byte biome = biomes[x + z * 16];
                    Color color;
                    if (Biomes[biome] != null)
                    {
                        color = Biomes[biome].Color;
                        toolTips[offsetX + x, offsetY + z] = Biomes[biome].Name;
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
       
        private static Color ColorLookup(int block, int damage, int blockAbove)
        {
            int ret = 0;

            switch (blockAbove)
            {
                case 6: //sapling
                    ret = 0x408f2f;
                    break;
                case 32: //dead bush
                    ret = 0x946428;
                    break;
                case 37: //yellow flower
                    ret = 0xf1f902;
                    break;
                case 38: //red flower
                    ret = 0xf7070f;
                    break;
                case 54: //chest
                    ret = 0x8e6525;
                    break;
                case 78: //snow cover
                    ret = 0xf1fcfc;
                    break;
                case 81: //cactus
                    ret = 0x0b5715;
                    break;
                case 83: //sugar cane
                    ret = 0xa0e080;
                    break;
                case 85: //fence
                case 107:
                    ret = 0xa08250;
                    break;
                case 111: //lily pad
                    ret = 0x13621c;
                    break;
                case 113: //nether fence
                    ret = 0x2d171b;
                    break;
                default:
                    ret = 0;
                    break;
            }

            if (ret != 0)
                return Color.FromArgb(255, Color.FromArgb(ret));

            switch (block)
            {
                case 1: //stone
                    ret = 0x7e7e7e;
                    break;
                case 2: //grass
                    ret = 0x62a238;
                    break;
                case 3: //dirt
                    ret = 0x866043;
                    break;
                case 4: //cobble
                case 67:
                    ret = 0x7c7c7c;
                    break;
                case 5: //planks
                    if (damage == 1) //pine
                        ret = 0x765834;
                    else if (damage == 2) //birch
                        ret = 0xd4c285;
                    else if (damage == 3) //jungle
                        ret = 0xa97b58;
                    else
                        ret = 0xa08250;
                    break;
                case 7: //bedrock
                    ret = 0x323232;
                    break;
                case 8: //water
                case 9:
                    ret = 0x2a5fff;
                    break;
                case 10: //lava
                case 11:
                    ret = 0xf74700;
                    break;
                case 12: //sand
                    ret = 0xdbd3a0;
                    break;
                case 13: //gravel
                    ret = 0x857d7d;
                    break;
                case 14: //gold ore
                    ret = 0x928e7d;
                    break;
                case 15: //iron ore
                    ret = 0x89837f;
                    break;
                case 16: //coal ore
                    ret = 0x727272;
                    break;
                case 17: //log
                    ret = 0xa38452;
                    break;
                case 18: //leaves
                    if (damage == 1 || damage == 5 || damage == 9) //pine
                        ret = 0x2d472d;
                    else if (damage == 2 || damage == 6 || damage == 10) //birch
                        ret = 0x074b36;
                    else if (damage == 3 || damage == 7 || damage == 11) //jungle
                        ret = 0x1c6f02;
                    else
                        ret = 0x2c5619;
                    break;
                case 19: //sponge
                    ret = 0xb5b538;
                    break;
                case 21: //lapis ore
                    ret = 0x636f88;
                    break;
                case 22: //lapis block
                    ret = 0x1d46a6;
                    break;
                case 23: //dispenser
                    ret = 0x676767;
                    break;
                case 24: //sandstone
                    ret = 0xdad29f;
                    break;
                case 25: //noteblock
                    ret = 0x6c4734;
                    break;
                case 29: //sticky piston
                    ret = 0x8d9462;
                    break;
                case 33: //piston
                    ret = 0x9b8157;
                    break;
                case 35: //wool
                    switch (damage)
                    {
                        case 1: //orange
                            ret = 0xea8037;
                            break;
                        case 2: //magenta
                            ret = 0xbf4cc9;
                            break;
                        case 3: //light blue
                            ret = 0x688bd4;
                            break;
                        case 4: //yellow
                            ret = 0xc2b51c;
                            break;
                        case 5: //lime green
                            ret = 0x3bbd30;
                            break;
                        case 6: //pink
                            ret = 0xd9849b;
                            break;
                        case 7: //dark gray
                            ret = 0x434343;
                            break;
                        case 8: //light gray
                            ret = 0x9ea6a6;
                            break;
                        case 9: //cyan
                            ret = 0x277596;
                            break;
                        case 10: //purple
                            ret = 0x8136c4;
                            break;
                        case 11: //blue
                            ret = 0x27339a;
                            break;
                        case 12: //brown
                            ret = 0x56331c;
                            break;
                        case 13: //green
                            ret = 0x384d18;
                            break;
                        case 14: //red
                            ret = 0xa42d29;
                            break;
                        case 15: //black
                            ret = 0x1b1717;
                            break;
                        default: //white
                            ret = 0xdfdfdf;
                            break;
                    }
                    break;
                case 41: //gold block
                    ret = 0xfbf152;
                    break;
                case 42: //iron block
                    ret = 0xe0e0e0;
                    break;
                case 43: //slabs
                case 44:
                    switch (damage)
                    {
                        case 1: //sandstone
                        case 9:
                            ret = 0xdad29f;
                            break;
                        case 2: //wood
                        case 10:
                            ret = 0xa08250;
                            break;
                        case 3: //cobble
                        case 11:
                            ret = 0x7c7c7c;
                            break;
                        case 4: //brick
                        case 12:
                            ret = 0x916052;
                            break;
                        case 5: //stone brick
                        case 13:
                            ret = 0x7d7d7d;
                            break;
                        default: //stone
                            ret = 0xa3a3a3;
                            break;
                    }
                    break;
                case 45: //brick
                case 108:
                    ret = 0x916052;
                    break;
                case 46: //tnt
                    ret = 0xad5238;
                    break;
                case 47: //bookshelve
                    ret = 0xa08250;
                    break;
                case 48: //mossy cobble
                    ret = 0x697b69;
                    break;
                case 49: //obsidian
                    ret = 0x241351;
                    break;
                case 53: //wood stairs
                    ret = 0xa08250;
                    break;
                case 54: //chest
                    ret = 0x8e6525;
                    break;
                case 56: //diamond ore
                    ret = 0x828f92;
                    break;
                case 57: //diamond block
                    ret = 0x6fdfda;
                    break;
                case 58: //workbench
                    ret = 0x764e2f;
                    break;
                case 60: //farmland
                    ret = 0x724b2d;
                    break;
                case 61: //furnace
                case 62:
                    ret = 0x676767;
                    break;
                case 73: //redstone ore
                case 74:
                    ret = 0x866969;
                    break;
                case 80: //snow block
                    ret = 0xf1fcfc;
                    break;
                case 79: //ice
                    ret = 0x7eaeff;
                    break;
                case 82: //clay
                    ret = 0x9fa4b1;
                    break;
                case 84: //jukebox
                    ret = 0x734d39;
                    break;
                case 86: //pumpkin
                case 91:
                    ret = 0xc27816;
                    break;
                case 87: //netherrak
                    ret = 0x703635;
                    break;
                case 88: //soul sand
                    ret = 0x543f33;
                    break;
                case 89: //glowstone
                    ret = 0x927848;
                    break;
                case 98: //stone brick
                case 109:
                    ret = 0x7d7d7d;
                    break;
                case 99: //huge brown mushroom
                    ret = 0x8d6a53;
                    break;
                case 100: //huge red mushroom
                    ret = 0xb72725;
                    break;
                case 103: //melon
                    ret = 0x969925;
                    break;
                case 110: //mycelium
                    ret = 0x6f6469;
                    break;
                case 112: //nether brick
                case 114:
                    ret = 0x2d171b;
                    break;
                case 121: //end stone
                    ret = 0xdddfa5;
                    break;
                case 123: //redstone lamp off
                    ret = 0x865536;
                    break;
                case  124: //redstone lamp on
                    ret = 0xd0a26b;
                    break;
                default:
                    ret = 0x000000;
                    break;
            }
            return Color.FromArgb(255, Color.FromArgb(ret));
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

        public static void Fill(RegionFile region, Bitmap selection, Color selectionColor, byte biome)
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
                        if (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
                        {
                            biomes[x + z * 16] = biome;
                        }
                    }
                }
            }
        }

        public static void Fill(RegionFile region, Bitmap selection, Color selectionColor, BiomeUtil util)
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
                        if (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
                        {
                            biomes[x + z * 16] = (byte)util.GetBiome(chunkAbs.X + x, chunkAbs.Z + z);
                        }
                    }
                }
            }
        }

        public static void Replace(RegionFile region, Bitmap selection, Color selectionColor, byte search, byte replace)
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
                        if (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
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

        public static void Replace(RegionFile region, Bitmap selection, Color selectionColor, byte search, BiomeUtil replace)
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
                        if (selection.GetPixel(OFFSETX + chunkOffset.X + x, OFFSETY + chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
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

                int[] heightmap = (int[])c.Root["Level"]["HeightMap"];
                Dictionary<int, TAG_Compound> sections = new Dictionary<int, TAG_Compound>();
                foreach (TAG t in (TAG[])c.Root["Level"]["Sections"])
                {
                    sections.Add((byte)t["Y"], (TAG_Compound)t);
                }

                //chunk exists but all blocks are air
                if (sections.Count == 0)
                    continue;

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int height = heightmap[z * 16 + x];

                        //trees runnning into the old height limit in converted worlds
                        //seem to cause the heightmap entries for its columns to be -128;
                        if (height < 0)
                            height = 128;

                        int sectionIndex = (int)Math.Floor((height - 1) / 16.0);
                        int sectionAboveIndex = (int)Math.Floor(height / 16.0);

                        int block = -1, damage = -1;
                        if (sections.ContainsKey(sectionIndex))
                        {
                            byte[] blocks = (byte[])sections[sectionIndex]["Blocks"];
                            byte[] data = (byte[])sections[sectionIndex]["Data"];
                            int blockOffset = ((((height - 1) % 16) * 16 + z) * 16 + x);
                            block = blocks[blockOffset];
                            damage = data[(int)Math.Floor(blockOffset / 2.0)];
                            if (blockOffset % 2 == 0)
                                damage = (damage >> 4) & 0x0F;
                            else
                                damage = damage & 0x0F;
                        }

                        int blockAbove = block;
                        if (sections.ContainsKey(sectionAboveIndex))
                        {
                            int blockAboveOffset = (((height % 16) * 16 + z) * 16 + x);
                            byte[] blocksAbove = (byte[])sections[sectionAboveIndex]["Blocks"];
                            blockAbove = blocksAbove[blockAboveOffset];
                        }

                        if(ids.Contains(blockAbove) || (blockAbove == 0 && ids.Contains(block)))
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
                g.CompositingMode = CompositingMode.SourceCopy;
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