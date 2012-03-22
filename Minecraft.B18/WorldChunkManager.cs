using System;

namespace Minecraft.B18
{
    public class WorldChunkManager : Minecraft.BiomeUtil
    {
        private BiomeCache biomeCache;
        private GenLayer temperatureLayer;
        private GenLayer rainfallLayer;
        private GenLayer field_34902_c;

        public WorldChunkManager(long seed)
            : base(seed)
        {
            biomeCache = new BiomeCache(this);

            GenLayer[] agenlayer = GenLayer.func_35497_a(seed);
            field_34902_c = agenlayer[1];
            temperatureLayer = agenlayer[2];
            rainfallLayer = agenlayer[3];
        }

        public override Biome GetBiome(int x, int z)
        {
            return (Biome)getBiomeGenAt(x, z).biomeID;
        }

        public BiomeGenBase getBiomeGenAt(int i, int j)
        {
            return biomeCache.func_35725_a(i, j);
        }

        public float[] getTemperatures(float[] af, int i, int j, int k, int l)
        {
            IntCache.func_35268_a();
            if (af == null || af.Length < k * l)
            {
                af = new float[k * l];
            }
            int[] ai = temperatureLayer.func_35500_a(i, j, k, l);
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
            IntCache.func_35268_a();
            if (af == null || af.Length < k * l)
            {
                af = new float[k * l];
            }
            int[] ai = rainfallLayer.func_35500_a(i, j, k, l);
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

        public BiomeGenBase[] func_35555_a(BiomeGenBase[] abiomegenbase, int i, int j, int k, int l, bool flag)
        {
            IntCache.func_35268_a();
            if (abiomegenbase == null || abiomegenbase.Length < k * l)
            {
                abiomegenbase = new BiomeGenBase[k * l];
            }
            if (flag && k == 16 && l == 16 && (i & 0xf) == 0 && (j & 0xf) == 0)
            {
                BiomeGenBase[] abiomegenbase1 = biomeCache.func_35723_d(i, j);
                Array.Copy(abiomegenbase1, 0, abiomegenbase, 0, k * l);
                return abiomegenbase;
            }
            int[] ai = field_34902_c.func_35500_a(i, j, k, l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                abiomegenbase[i1] = BiomeGenBase.biomeList[ai[i1]];
            }

            return abiomegenbase;
        }

    }
}
