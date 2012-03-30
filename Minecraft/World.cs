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
    }
}
