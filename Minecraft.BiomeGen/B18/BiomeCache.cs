using System;
using System.Collections;

namespace Minecraft.BiomeGen.B18
{
    public class BiomeCache
    {
        private WorldChunkManager chunkmanager;
        private LongHashMap field_35730_c;
        private ArrayList field_35728_d;

        public BiomeCache(WorldChunkManager worldchunkmanager)
        {
            chunkmanager = worldchunkmanager;
            field_35730_c = new LongHashMap();
            field_35728_d = new ArrayList();
        }

        public BiomeGenBase func_35725_a(int i, int j)
        {
            return getBiomeCacheBlock(i, j).func_35651_a(i, j);
        }

        public BiomeGenBase[] func_35723_d(int i, int j)
        {
            return getBiomeCacheBlock(i, j).field_35658_c;
        }

        private BiomeCacheBlock getBiomeCacheBlock(int i, int j)
        {
            i >>= 4;
            j >>= 4;
            long l = (long)i & 0xffffffffL | ((long)j & 0xffffffffL) << 32;
            BiomeCacheBlock biomecacheblock = (BiomeCacheBlock)field_35730_c.getValueByKey(l);
            if (biomecacheblock == null)
            {
                biomecacheblock = new BiomeCacheBlock(this, i, j);
                field_35730_c.add(l, biomecacheblock);
                field_35728_d.Add(biomecacheblock);
            }
            biomecacheblock.field_35653_f = Convert.ToInt64(((TimeSpan)(DateTime.UtcNow - new DateTime(1970, 1, 1))).TotalMilliseconds);
            return biomecacheblock;
        }

        public static WorldChunkManager getWorldChunkManager(BiomeCache biomecache)
        {
            return biomecache.chunkmanager;
        }


    }
}
