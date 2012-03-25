using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Ionic.Zlib;

namespace Minecraft
{
    public class World
    {
        public long Seed;
        public String RegionDir;

        public World(String path)
        {
            TAG_Compound data;

            using (FileStream level = new FileStream(path, FileMode.Open))
            {
                using (GZipStream decompress = new GZipStream(level, CompressionMode.Decompress))
                {
                    MemoryStream mem = new MemoryStream();
                    decompress.CopyTo(mem);
                    mem.Seek(0, SeekOrigin.Begin);
                    data = new TAG_Compound(mem);
                }
            }

            Seed = (long)data["Data"]["RandomSeed"];
            RegionDir = String.Format("{0}{1}region", Path.GetDirectoryName(path), Path.DirectorySeparatorChar);
        }

        public String[] GetRegionPaths()
        {
            return Directory.GetFiles(RegionDir, "*.mca", SearchOption.TopDirectoryOnly);
        }

        public void RenderRegion(RegionFile region, Bitmap b)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.Black);
            }

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

                for (int z = 0; z < 16; z++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        int height = heightmap[z * 16 + x];
                        
                        //trees runnning into the old height limit in converted worlds
                        //seem to cause the heightmap entries for its columns to be -128
                        if (height < 0)
                        {
                            height = 128;
                        }
                        
                        byte[] blocks = (byte[])sections[(int)Math.Floor((height - 1) / 16.0)]["Blocks"];
                        byte[] data = (byte[])sections[(int)Math.Floor((height - 1) / 16.0)]["Data"];
                        int blockOffset = ((((height - 1) % 16) * 16 + z) * 16 + x);
                        int block = blocks[blockOffset];
                        int damage = data[(int)Math.Floor(blockOffset / 2.0)];
                        if (blockOffset % 2 == 0)
                            damage = (damage >> 4) & 0x0F;
                        else
                            damage = damage & 0x0F;

                        int blockAboveOffset = (((height % 16) * 16 + z) * 16 + x);
                        int blockAbove = block;
                        if (height < 256 && sections.ContainsKey((int)Math.Floor(height / 16.0)))
                        {
                            byte[] blocksAbove = (byte[])sections[(int)Math.Floor(height / 16.0)]["Blocks"];
                            blockAbove = blocksAbove[blockAboveOffset];
                        }

                        b.SetPixel(chunkOffset.X + x, chunkOffset.Z + z, ColorLookup(block, damage, blockAbove));
                    }
                }
            }
        }

        public void RenderRegionBiomes(RegionFile region, Bitmap b, String[,] toolTips)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.Black);
            }

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
                        Biome biome = (Biome)biomes[x + z * 16];
                        Color color;
                        switch (biome)
                        {
                            case Biome.Ocean:
                                color = Color.Blue;
                                break;
                            case Biome.Plains:
                                color = Color.PaleGreen;
                                break;
                            case Biome.Desert:
                            case Biome.DesertHills:
                                color = Color.BurlyWood;
                                break;
                            case Biome.ExtremeHills:
                            case Biome.ExtremeHillsEdge:
                                color = Color.DarkGreen;
                                break;
                            case Biome.Forest:
                            case Biome.ForestHills:
                                color = Color.Green;
                                break;
                            case Biome.Taiga:
                            case Biome.TaigaHills:
                                color = Color.SeaGreen;
                                break;
                            case Biome.Swampland:
                                color = Color.DarkCyan;
                                break;
                            case Biome.River:
                                color = Color.CornflowerBlue;
                                break;
                            case Biome.Hell:
                                color = Color.Firebrick;
                                break;
                            case Biome.Sky:
                                color = Color.Gray;
                                break;
                            case Biome.FrozenOcean:
                                color = Color.DeepSkyBlue;
                                break;
                            case Biome.FrozenRiver:
                                color = Color.LightSkyBlue;
                                break;
                            case Biome.IcePlains:
                                color = Color.White;
                                break;
                            case Biome.IceMountains:
                                color = Color.LightGray;
                                break;
                            case Biome.MushroomIsland:
                            case Biome.MushroomIslandShore:
                                color = Color.Teal;
                                break;
                            case Biome.Beach:
                                color = Color.Beige;
                                break;
                            case Biome.Jungle:
                            case Biome.JungleHills:
                                color = Color.LimeGreen;
                                break;
                            default:
                                color = Color.Black;
                                break;
                        }
                        b.SetPixel(chunkOffset.X + x, chunkOffset.Z + z, color);
                        toolTips[chunkOffset.X + x, chunkOffset.Z + z] = biome.ToString();
                    }
                }
            }
        }

        private Color ColorLookup(int block, int damage, int blockAbove)
        {
            int ret = 0;

            switch (blockAbove)
            {
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
                case 53:
                    ret = 0xa08250;
                    break;
                case 7: //bedrock
                    ret = 0x555555;
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
                    if (damage == 1) //pine
                        ret = 0x2d472d;
                    else if (damage == 2) //birch
                        ret = 0x074b36;
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
                            ret = 0xdad29f;
                            break;
                        case 2: //wood
                            ret = 0xa08250;
                            break;
                        case 3: //cobble
                            ret = 0x7c7c7c;
                            break;
                        case 4: //brick
                            ret = 0x916052;
                            break;
                        case 5: //stone brick
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
                    ret = 0x15131f;
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
                default:
                    ret = 0x000000;
                    break;
            }
            return Color.FromArgb(255, Color.FromArgb(ret));
        }

        public void Fill(RegionFile region, Bitmap selection, Color selectionColor, Biome biome)
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
                        if (selection.GetPixel(chunkOffset.X + x, chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
                        {
                            biomes[x + z * 16] = (byte)biome;
                            c.Dirty = true;
                            region.Dirty = true;
                        }
                    }
                }
            }
        }

        public void Fill(RegionFile region, Bitmap selection, Color selectionColor, BiomeUtil util)
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
                        if (selection.GetPixel(chunkOffset.X + x, chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
                        {
                            biomes[x + z * 16] = (byte)util.GetBiome(chunkAbs.X + x, chunkAbs.Z + z);
                            c.Dirty = true;
                            region.Dirty = true;
                        }
                    }
                }
            }
        }

        public void Replace(RegionFile region, Bitmap selection, Color selectionColor, Biome search, Biome replace)
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
                        if (selection.GetPixel(chunkOffset.X + x, chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
                        {
                            if (biomes[x + z * 16] == (byte)search)
                            {
                                biomes[x + z * 16] = (byte)replace;
                                c.Dirty = true;
                                region.Dirty = true;
                            }
                        }
                    }
                }
            }
        }

        public void Replace(RegionFile region, Bitmap selection, Color selectionColor, Biome search, BiomeUtil replace)
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
                        if (selection.GetPixel(chunkOffset.X + x, chunkOffset.Z + z).ToArgb() == selectionColor.ToArgb())
                        {
                            if (biomes[x + z * 16] == (byte)search)
                            {
                                biomes[x + z * 16] = (byte)replace.GetBiome(chunkAbs.X + x, chunkAbs.Z + z);
                                c.Dirty = true;
                                region.Dirty = true;
                            }
                        }
                    }
                }
            }
        }

        public static void SelectChunks(Bitmap b, Color selectionColor)
        {
            using (Graphics g = Graphics.FromImage(b))
            {
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
                                if (b.GetPixel(chunkX * 16 + x, chunkZ * 16 + z).ToArgb() == selectionColor.ToArgb())
                                {
                                    shouldSelect = true;
                                    break;
                                }
                            }
                            if (shouldSelect)
                                break;
                        }

                        if (shouldSelect)
                            g.FillRectangle(brush, chunkX * 16, chunkZ * 16, 16, 16);
                    }
                }
            }
        }
    }
}
