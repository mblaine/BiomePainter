
namespace Minecraft.BiomeGen.F12
{
    public class GenLayerAddSnow : GenLayer
    {
        public GenLayerAddSnow(long par1, GenLayer par3GenLayer)
            : base(par1)
        {
            parent = par3GenLayer;
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int i = par1 - 1;
            int j = par2 - 1;
            int k = par3 + 2;
            int l = par4 + 2;
            int[] ai = parent.getInts(i, j, k, l);
            int[] ai1 = IntCache.getIntCache(par3 * par4);

            for (int i1 = 0; i1 < par4; i1++)
            {
                for (int j1 = 0; j1 < par3; j1++)
                {
                    int k1 = ai[j1 + 1 + (i1 + 1) * k];
                    initChunkSeed(j1 + par1, i1 + par2);

                    if (k1 == 0)
                    {
                        ai1[j1 + i1 * par3] = 0;
                        continue;
                    }

                    int l1 = nextInt(5);

                    if (l1 == 0)
                    {
                        l1 = BiomeGenBase.icePlains.biomeID;
                    }
                    else
                    {
                        l1 = 1;
                    }

                    ai1[j1 + i1 * par3] = l1;
                }
            }

            return ai1;
        }
    }
}
