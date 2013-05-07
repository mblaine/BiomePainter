using System;
using System.Collections;

namespace Minecraft.BiomeGen.F15
{
    public class BiomeCache
    {
        private WorldChunkManager chunkManager;

        private LongHashMap cacheMap = new LongHashMap();

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