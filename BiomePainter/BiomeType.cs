using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BiomePainter
{
    public class BiomeType : IComparable
    {
        public byte ID;
        public String Name;
        public Color Color;

        private static BiomeType[] biomes = null;

        private static String path = String.Format("{0}{1}Biomes.txt", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Path.DirectorySeparatorChar);

        public static BiomeType[] Biomes
        {
            get
            {
                if (biomes == null)
                {
                    biomes = new BiomeType[256];
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
                                    biomes[id] = new BiomeType(id, m.Groups[2].Value, Convert.ToInt32(m.Groups[3].Value, 16));
                                }
                            }
                            #if DEBUG
                            else
                            {
                                if (line.Trim().Length > 0)
                                    throw new Exception(String.Format("Malformed line:\"{0}\"", line));
                            }
                            #endif
                        }
                    }

                    Biomes[255] = new BiomeType(255, "Unspecified", 0x000000);
                }
                return biomes;
            }
        }

        public BiomeType(byte id, String name, int color)
        {
            ID = id;
            Name = name;
            Color = Color.FromArgb(255, Color.FromArgb(color));
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            if (obj is BiomeType)
                return Name.CompareTo(((BiomeType)obj).Name);
            else
                return 0;
        }
    }
}
