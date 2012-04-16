using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Ionic.Zlib;

namespace Minecraft
{
    public class World
    {
        public long Seed;
        public String WorldDir;

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
            WorldDir = Path.GetDirectoryName(path);
        }

        public String GetRegionDirectory(Dimension dim)
        {
            switch (dim)
            {
                case Dimension.Overworld:
                    return String.Format("{0}{1}region", WorldDir, Path.DirectorySeparatorChar);
                case Dimension.Nether:
                    return String.Format("{0}{1}DIM-1{1}region", WorldDir, Path.DirectorySeparatorChar);
                case Dimension.End:
                    return String.Format("{0}{1}DIM1{1}region", WorldDir, Path.DirectorySeparatorChar);
                default:
                    throw new Exception("Unrecognized dimension.");
            }
        }

        public String[] GetRegionPaths(Dimension dim = Dimension.Overworld)
        {
            String dir = GetRegionDirectory(dim);
            if (Directory.Exists(dir))
            {

                List<String> regions = new List<String>(Directory.GetFiles(dir, "*.mca", SearchOption.TopDirectoryOnly));
                regions.Sort(CompareRegionNames);
                return regions.ToArray();
            }
            else
                return new String[0];
        }

        private static int CompareRegionNames(String r1, String r2)
        {
            Regex pattern = new Regex(@"r\.(-?\d+)\.(-?\d+)\.mca");
            Match m = pattern.Match(r1);
            int x1 = int.Parse(m.Groups[1].Value);
            int z1 = int.Parse(m.Groups[2].Value);
            m = pattern.Match(r2);
            int x2 = int.Parse(m.Groups[1].Value);
            int z2 = int.Parse(m.Groups[2].Value);

            if (x1 < x2)
                return -1;
            else if (x2 < x1)
                return 1;
            else
            {
                if (z1 < z2)
                    return -1;
                else if (z2 < z1)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
