
namespace Minecraft.BiomeGen.B18
{
    public class GenLayerVillageLandscape : GenLayer
    {
            private BiomeGenBase[] allowedBiomes;

            public GenLayerVillageLandscape(long l, GenLayer genlayer)
                : base(l)
            {
                allowedBiomes = (new BiomeGenBase[] { BiomeGenBase.desert, BiomeGenBase.forest, BiomeGenBase.hills, BiomeGenBase.swampland, BiomeGenBase.plains, BiomeGenBase.taiga });
                parent = genlayer;
            }

            public override int[] func_35500_a(int i, int j, int k, int l)
            {
                int[] ai = parent.func_35500_a(i, j, k, l);
                int[] ai1 = IntCache.getIntCache(k * l);
                for (int i1 = 0; i1 < l; i1++)
                {
                    for (int j1 = 0; j1 < k; j1++)
                    {
                        func_35499_a(j1 + i, i1 + j);
                        ai1[j1 + i1 * k] = ai[j1 + i1 * k] <= 0 ? 0 : allowedBiomes[nextInt(allowedBiomes.Length)].biomeID;
                    }
                }

                return ai1;
            }
    }
}
