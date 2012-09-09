using System;

namespace Minecraft.BiomeGen.F12
{
    public class WorldChunkManager : Minecraft.BiomeUtil
    {
        /** A GenLayer containing the indices into BiomeGenBase.biomeList[] */
        private GenLayer biomeIndexLayer;

        private BiomeCache biomeCache;

        public WorldChunkManager(long seed)
            : base(seed)
        {
            biomeCache = new BiomeCache(this);

            GenLayer[] agenlayer = GenLayer.func_48425_a(Seed/*, par3WorldType*/);
            biomeIndexLayer = agenlayer[1];
        }

        public override Biome GetBiome(int x, int z)
        {
            return (Biome)getBiomeGenAt(x, z).biomeID;
        }

        public BiomeGenBase getBiomeGenAt(int par1, int par2)
        {
            return biomeCache.getBiomeGenAt(par1, par2);
        }

        /**
         * Returns a list of temperatures to use for the specified blocks.  Args: listToReuse, x, y, width, length
         */
        public float[] getTemperatures(float[] par1ArrayOfFloat, int par2, int par3, int par4, int par5)
        {
            IntCache.resetIntCache();

            if (par1ArrayOfFloat == null || par1ArrayOfFloat.Length < par4 * par5)
            {
                par1ArrayOfFloat = new float[par4 * par5];
            }

            int[] ai = biomeIndexLayer.getInts(par2, par3, par4, par5);

            for (int i = 0; i < par4 * par5; i++)
            {
                float f = (float)BiomeGenBase.biomeList[ai[i]].getIntTemperature() / 65536F;

                if (f > 1.0F)
                {
                    f = 1.0F;
                }

                par1ArrayOfFloat[i] = f;
            }

            return par1ArrayOfFloat;
        }

        /**
         * Returns a list of rainfall values for the specified blocks. Args: listToReuse, x, z, width, length.
         */
        public float[] getRainfall(float[] par1ArrayOfFloat, int par2, int par3, int par4, int par5)
        {
            IntCache.resetIntCache();

            if (par1ArrayOfFloat == null || par1ArrayOfFloat.Length < par4 * par5)
            {
                par1ArrayOfFloat = new float[par4 * par5];
            }

            int[] ai = biomeIndexLayer.getInts(par2, par3, par4, par5);

            for (int i = 0; i < par4 * par5; i++)
            {
                float f = (float)BiomeGenBase.biomeList[ai[i]].getIntRainfall() / 65536F;

                if (f > 1.0F)
                {
                    f = 1.0F;
                }

                par1ArrayOfFloat[i] = f;
            }

            return par1ArrayOfFloat;
        }

        /**
         * Return a list of biomes for the specified blocks. Args: listToReuse, x, y, width, length, cacheFlag (if false,
         * don't check biomeCache to avoid infinite loop in BiomeCacheBlock)
         */
        public BiomeGenBase[] getBiomeGenAt(BiomeGenBase[] par1ArrayOfBiomeGenBase, int par2, int par3, int par4, int par5, bool par6)
        {
            IntCache.resetIntCache();

            if (par1ArrayOfBiomeGenBase == null || par1ArrayOfBiomeGenBase.Length < par4 * par5)
            {
                par1ArrayOfBiomeGenBase = new BiomeGenBase[par4 * par5];
            }

            if (par6 && par4 == 16 && par5 == 16 && (par2 & 0xf) == 0 && (par3 & 0xf) == 0)
            {
                BiomeGenBase[] abiomegenbase = biomeCache.getCachedBiomes(par2, par3);
                Array.Copy(abiomegenbase, 0, par1ArrayOfBiomeGenBase, 0, par4 * par5);
                return par1ArrayOfBiomeGenBase;
            }

            int[] ai = biomeIndexLayer.getInts(par2, par3, par4, par5);

            for (int i = 0; i < par4 * par5; i++)
            {
                par1ArrayOfBiomeGenBase[i] = BiomeGenBase.biomeList[ai[i]];
            }

            return par1ArrayOfBiomeGenBase;
        }
    }
}
