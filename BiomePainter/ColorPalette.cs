using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Minecraft;

namespace BiomePainter
{
    public class ColorPalette
    {
        private static Dictionary<String, Color> blockTable = null;

        private static String pathDefault = String.Format("{0}{1}Blocks.default.txt", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.DirectorySeparatorChar);
        private static String pathUser = String.Format("{0}{1}Blocks.user.txt", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.DirectorySeparatorChar);

        private static void LoadBlockTable()
        {
            blockTable = new Dictionary<String, Color>();

            if(File.Exists(pathDefault))
                Parse(pathDefault);
            if(File.Exists(pathUser))
                Parse(pathUser);
        }

        private static void Parse(String path)
        {
            Regex linePattern = new Regex(@"^([0-9:,]+);(?:([0-9,]+);)?([0-9a-fA-F]{6,8})\s*(?:#.*)?$");
            Regex idPattern = new Regex(@"^\d+(:?\:\d+)?$");
            Regex biomePattern = new Regex(@"^\d+$");

            String[] lines = File.ReadAllLines(path);

            foreach (String line in lines)
            {
                Match m = linePattern.Match(line);
                if (m.Groups.Count >= 3)
                {
                    Color color = Color.FromArgb(Convert.ToInt32(m.Groups[m.Groups.Count - 1].Value.PadLeft(8, 'f'), 16));

                    List<String> append = new List<string>();
                    if (m.Groups.Count == 4)
                    {
                        String[] biomes = m.Groups[2].Value.Split(',');
                        foreach (String biome in biomes)
                        {
                            if (biomePattern.IsMatch(biome))
                            {
                                append.Add(biome);
                            }
                        }
                    }

                    String[] ids = m.Groups[1].Value.Split(',');
                    foreach (String id in ids)
                    {
                        if (idPattern.IsMatch(id))
                        {
                            if (append.Count > 0)
                            {
                                foreach (String s in append)
                                {
                                    String key = String.Format("{0}b{1}", id, s);

                                    if (!blockTable.ContainsKey(key))
                                        blockTable.Add(key, color);
                                    else
                                        blockTable[key] = color;
                                }
                            }
                            else
                            {
                                if (!blockTable.ContainsKey(id))
                                    blockTable.Add(id, color);
                                else
                                    blockTable[id] = color;
                            }
                        }
                    }
                }
                #if DEBUG
                else
                {
                    if (line.Trim().Length > 0 && line[0] != '#')
                        throw new Exception(String.Format("Malformed line:\"{0}\"", line));
                }
                #endif
            }
        }

        public static void Reset()
        {
            blockTable = null;
        }

        public static void Preload()
        {
            if (blockTable == null)
                LoadBlockTable();
        }

        public static Color Lookup(int block, int data, byte biome)
        {
            if (blockTable == null)
                LoadBlockTable();

            String idDataBiome = String.Format("{0}:{1}b{2}", block, data, biome);
            String idData = String.Format("{0}:{1}", block, data);
            String idBiome = String.Format("{0}b{1}", block, biome);
            String id = block.ToString();

            if (biome != (byte)Biome.Unspecified && blockTable.ContainsKey(idDataBiome))
                return blockTable[idDataBiome];
            else if (blockTable.ContainsKey(idData))
                return blockTable[idData];
            else if (biome != (byte)Biome.Unspecified && blockTable.ContainsKey(idBiome))
                return blockTable[idBiome];
            else if (blockTable.ContainsKey(id))
                return blockTable[id];
            else
                return Color.FromArgb(0xc3, 0x74, 0xe9); //magenta
        }
    }
}