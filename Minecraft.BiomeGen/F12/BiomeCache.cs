using System;
using System.Collections;

namespace Minecraft.F12
{
    public class BiomeCache
    {
        private WorldChunkManager chunkManager;

        /**
         * The map of keys to BiomeCacheBlocks. Keys are based on the chunk x, z coordinates as (x | z << 32).
         */
        private LongHashMap cacheMap;

        /** The list of cached BiomeCacheBlocks */
        private ArrayList cache;

        public BiomeCache(WorldChunkManager par1WorldChunkManager)
        {
            cacheMap = new LongHashMap();
            cache = new ArrayList();
            chunkManager = par1WorldChunkManager;
        }

        public BiomeCacheBlock getBiomeCacheBlock(int par1, int par2)
        {
            par1 >>= 4;
            par2 >>= 4;
            long l = (long)par1 & 0xffffffffL | ((long)par2 & 0xffffffffL) << 32;
            BiomeCacheBlock biomecacheblock = (BiomeCacheBlock)cacheMap.getValueByKey(l);

            if (biomecacheblock == null)
            {
                biomecacheblock = new BiomeCacheBlock(this, par1, par2);
                cacheMap.add(l, biomecacheblock);
                cache.Add(biomecacheblock);
            }

            biomecacheblock.lastAccessTime = Convert.ToInt64(((TimeSpan)(DateTime.UtcNow - new DateTime(1970, 1, 1))).TotalMilliseconds);
            return biomecacheblock;
        }

        public BiomeGenBase getBiomeGenAt(int par1, int par2)
        {
            return getBiomeCacheBlock(par1, par2).getBiomeGenAt(par1, par2);
        }

        public BiomeGenBase[] getCachedBiomes(int par1, int par2)
        {
            return getBiomeCacheBlock(par1, par2).biomes;
        }

        public static WorldChunkManager getChunkManager(BiomeCache par0BiomeCache)
        {
            return par0BiomeCache.chunkManager;
        }
    }
}
