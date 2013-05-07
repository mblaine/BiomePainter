using System;

namespace Minecraft.BiomeGen.F15
{
    public class WorldChunkManager : Minecraft.BiomeUtil
    {
        public static bool LargeBiomes = false;

        private GenLayer biomeIndexLayer;
        private BiomeCache biomeCache;

        public WorldChunkManager(long seed)
            : base(seed)
        {
            biomeCache = new BiomeCache(this);
            GenLayer[] var4 = GenLayer.initializeAllBiomeGenerators(Seed, LargeBiomes);
            this.biomeIndexLayer = var4[1];
        }

        public override Biome GetBiome(int x, int z)
        {
            return (Biome)getBiomeGenAt(x, z).biomeID;
        }
        public BiomeGenBase getBiomeGenAt(int par1, int par2)
        {
            return this.biomeCache.getBiomeGenAt(par1, par2);
        }

        public float[] getRainfall(float[] par1ArrayOfFloat, int par2, int par3, int par4, int par5)
        {
            IntCache.resetIntCache();

            if (par1ArrayOfFloat == null || par1ArrayOfFloat.Length < par4 * par5)
            {
                par1ArrayOfFloat = new float[par4 * par5];
            }

            int[] var6 = this.biomeIndexLayer.getInts(par2, par3, par4, par5);

            for (int var7 = 0; var7 < par4 * par5; ++var7)
            {
                float var8 = (float)BiomeGenBase.biomeList[var6[var7]].getIntRainfall() / 65536.0F;

                if (var8 > 1.0F)
                {
                    var8 = 1.0F;
                }

                par1ArrayOfFloat[var7] = var8;
            }

            return par1ArrayOfFloat;
        }

        public float[] getTemperatures(float[] par1ArrayOfFloat, int par2, int par3, int par4, int par5)
        {
            IntCache.resetIntCache();

            if (par1ArrayOfFloat == null || par1ArrayOfFloat.Length < par4 * par5)
            {
                par1ArrayOfFloat = new float[par4 * par5];
            }

            int[] var6 = this.biomeIndexLayer.getInts(par2, par3, par4, par5);

            for (int var7 = 0; var7 < par4 * par5; ++var7)
            {
                float var8 = (float)BiomeGenBase.biomeList[var6[var7]].getIntTemperature() / 65536.0F;

                if (var8 > 1.0F)
                {
                    var8 = 1.0F;
                }

                par1ArrayOfFloat[var7] = var8;
            }

            return par1ArrayOfFloat;
        }

        public BiomeGenBase[] getBiomeGenAt(BiomeGenBase[] par1ArrayOfBiomeGenBase, int par2, int par3, int par4, int par5, bool par6)
        {
            IntCache.resetIntCache();

            if (par1ArrayOfBiomeGenBase == null || par1ArrayOfBiomeGenBase.Length < par4 * par5)
            {
                par1ArrayOfBiomeGenBase = new BiomeGenBase[par4 * par5];
            }

            if (par6 && par4 == 16 && par5 == 16 && (par2 & 15) == 0 && (par3 & 15) == 0)
            {
                BiomeGenBase[] var9 = this.biomeCache.getCachedBiomes(par2, par3);
                Array.Copy(var9, 0, par1ArrayOfBiomeGenBase, 0, par4 * par5);
                return par1ArrayOfBiomeGenBase;
            }
            else
            {
                int[] var7 = this.biomeIndexLayer.getInts(par2, par3, par4, par5);

                for (int var8 = 0; var8 < par4 * par5; ++var8)
                {
                    par1ArrayOfBiomeGenBase[var8] = BiomeGenBase.biomeList[var7[var8]];
                }

                return par1ArrayOfBiomeGenBase;
            }
        }


    }
}