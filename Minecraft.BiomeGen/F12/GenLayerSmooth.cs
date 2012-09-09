
namespace Minecraft.BiomeGen.F12
{
    public class GenLayerSmooth : GenLayer
    {
        public GenLayerSmooth(long par1, GenLayer par3GenLayer)
            : base(par1)
        {
            base.parent = par3GenLayer;
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
                    int k1 = ai[j1 + 0 + (i1 + 1) * k];
                    int l1 = ai[j1 + 2 + (i1 + 1) * k];
                    int i2 = ai[j1 + 1 + (i1 + 0) * k];
                    int j2 = ai[j1 + 1 + (i1 + 2) * k];
                    int k2 = ai[j1 + 1 + (i1 + 1) * k];

                    if (k1 == l1 && i2 == j2)
                    {
                        initChunkSeed(j1 + par1, i1 + par2);

                        if (nextInt(2) == 0)
                        {
                            k2 = k1;
                        }
                        else
                        {
                            k2 = i2;
                        }
                    }
                    else
                    {
                        if (k1 == l1)
                        {
                            k2 = k1;
                        }

                        if (i2 == j2)
                        {
                            k2 = i2;
                        }
                    }

                    ai1[j1 + i1 * par3] = k2;
                }
            }

            return ai1;
        }
    }
}
