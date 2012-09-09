using System;

namespace Minecraft.BiomeGen.F10
{
    public class WorldChunkManager : Minecraft.BiomeUtil
    {
        private BiomeCache biomeCache;
        private GenLayer biomeIndexLayer;
        private GenLayer temperatureLayer;
        private GenLayer rainfallLayer;


        public WorldChunkManager(long seed)
            : base(seed)
        {
            biomeCache = new BiomeCache(this);
            GenLayer[] agenlayer = GenLayer.func_35497_a(Seed);
            biomeIndexLayer = agenlayer[1];
            temperatureLayer = agenlayer[2];
            rainfallLayer = agenlayer[3];
        }

        public override Biome GetBiome(int x, int z)
        {
            return (Biome)getBiomeGenAt(x, z).biomeID;
        }

        public BiomeGenBase getBiomeGenAt(int i, int j)
        {
            return biomeCache.getBiomeGenAt(i, j);
        }

        public float[] getTemperatures(float[] af, int i, int j, int k, int l)
        {
            IntCache.resetIntCache();
            if (af == null || af.Length < k * l)
            {
                af = new float[k * l];
            }
            int[] ai = temperatureLayer.getInts(i, j, k, l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                float f = (float)ai[i1] / 65536F;
                if (f > 1.0F)
                {
                    f = 1.0F;
                }
                af[i1] = f;
            }

            return af;
        }

        public float[] getRainfall(float[] af, int i, int j, int k, int l)
        {
            IntCache.resetIntCache();
            if (af == null || af.Length < k * l)
            {
                af = new float[k * l];
            }
            int[] ai = rainfallLayer.getInts(i, j, k, l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                float f = (float)ai[i1] / 65536F;
                if (f > 1.0F)
                {
                    f = 1.0F;
                }
                af[i1] = f;
            }

            return af;
        }

        public BiomeGenBase[] getBiomeGenAt(BiomeGenBase[] abiomegenbase, int i, int j, int k, int l, bool flag)
        {
            IntCache.resetIntCache();
            if (abiomegenbase == null || abiomegenbase.Length < k * l)
            {
                abiomegenbase = new BiomeGenBase[k * l];
            }
            if (flag && k == 16 && l == 16 && (i & 0xf) == 0 && (j & 0xf) == 0)
            {
                BiomeGenBase[] abiomegenbase1 = biomeCache.getCachedBiomes(i, j);
                Array.Copy(abiomegenbase1, 0, abiomegenbase, 0, k * l);
                return abiomegenbase;
            }
            int[] ai = biomeIndexLayer.getInts(i, j, k, l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                abiomegenbase[i1] = BiomeGenBase.biomeList[ai[i1]];
            }

            return abiomegenbase;
        }

    }
}
