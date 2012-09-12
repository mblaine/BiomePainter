using System;
using System.Collections;

namespace Minecraft.BiomeGen.F13
{
    public class BiomeCache
    {
        private WorldChunkManager chunkManager;

        /**
         * The map of keys to BiomeCacheBlocks. Keys are based on the chunk x, z coordinates as (x | z << 32).
         */
        private LongHashMap cacheMap = new LongHashMap();

        /** The ArrayList of cached BiomeCacheBlocks */
        private ArrayList cache = new ArrayList();

        public BiomeCache(WorldChunkManager par1WorldChunkManager)
        {
            this.chunkManager = par1WorldChunkManager;
        }

        public BiomeCacheBlock getBiomeCacheBlock(int par1, int par2)
        {
            par1 >>= 4;
            par2 >>= 4;
            long var3 = (long)par1 & 4294967295L | ((long)par2 & 4294967295L) << 32;
            BiomeCacheBlock var5 = (BiomeCacheBlock)this.cacheMap.getValueByKey(var3);

            if (var5 == null)
            {
                var5 = new BiomeCacheBlock(this, par1, par2);
                this.cacheMap.add(var3, var5);
                this.cache.Add(var5);
            }

            var5.lastAccessTime = Convert.ToInt64(((TimeSpan)(DateTime.UtcNow - new DateTime(1970, 1, 1))).TotalMilliseconds);
            return var5;
        }

        public BiomeGenBase getBiomeGenAt(int par1, int par2)
        {
            return this.getBiomeCacheBlock(par1, par2).getBiomeGenAt(par1, par2);
        }

        /**
         * Returns the array of cached biome types in the BiomeCacheBlock at the given location.
         */
        public BiomeGenBase[] getCachedBiomes(int par1, int par2)
        {
            return this.getBiomeCacheBlock(par1, par2).biomes;
        }


        public static WorldChunkManager getChunkManager(BiomeCache par0BiomeCache)
        {
            return par0BiomeCache.chunkManager;
        }
    }
}
