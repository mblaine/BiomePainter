using System;
using System.IO;
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
            if(Directory.Exists(dir))
                return Directory.GetFiles(dir, "*.mca", SearchOption.TopDirectoryOnly);
            else
                return new String[0];
        }
    }
}
