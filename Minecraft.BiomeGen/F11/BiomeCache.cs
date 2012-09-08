using System;
using System.Collections;

namespace Minecraft.F11
{
    public class BiomeCache
    {
        private WorldChunkManager chunkmanager;
        private LongHashMap cacheMap;
        private ArrayList cache;

        public BiomeCache(WorldChunkManager worldchunkmanager)
        {
            cacheMap = new LongHashMap();
            cache = new ArrayList();
            chunkmanager = worldchunkmanager;
        }

        public BiomeCacheBlock getBiomeCacheBlock(int i, int j)
        {
            i >>= 4;
            j >>= 4;
            long l = (long)i & 0xffffffffL | ((long)j & 0xffffffffL) << 32;
            BiomeCacheBlock biomecacheblock = (BiomeCacheBlock)cacheMap.getValueByKey(l);
            if (biomecacheblock == null)
            {
                biomecacheblock = new BiomeCacheBlock(this, i, j);
                cacheMap.add(l, biomecacheblock);
                cache.Add(biomecacheblock);
            }
            biomecacheblock.lastAccessTime = Convert.ToInt64(((TimeSpan)(DateTime.UtcNow - new DateTime(1970, 1, 1))).TotalMilliseconds);
            return biomecacheblock;
        }

        public BiomeGenBase getBiomeGenAt(int i, int j)
        {
            return getBiomeCacheBlock(i, j).getBiomeGenAt(i, j);
        }

        public BiomeGenBase[] getCachedBiomes(int i, int j)
        {
            return getBiomeCacheBlock(i, j).biomes;
        }

        public static WorldChunkManager getWorldChunkManager(BiomeCache biomecache)
        {
            return biomecache.chunkmanager;
        }
    }
}
