
namespace Minecraft.BiomeGen.F12
{
    public class GenLayerSwampRivers : GenLayer
    {
        public GenLayerSwampRivers(long par1, GenLayer par3GenLayer)
            : base(par1)
        {
            parent = par3GenLayer;
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int[] ai = parent.getInts(par1 - 1, par2 - 1, par3 + 2, par4 + 2);
            int[] ai1 = IntCache.getIntCache(par3 * par4);

            for (int i = 0; i < par4; i++)
            {
                for (int j = 0; j < par3; j++)
                {
                    initChunkSeed(j + par1, i + par2);
                    int k = ai[j + 1 + (i + 1) * (par3 + 2)];

                    if (k == BiomeGenBase.swampland.biomeID && nextInt(6) == 0)
                    {
                        ai1[j + i * par3] = BiomeGenBase.river.biomeID;
                        continue;
                    }

                    if ((k == BiomeGenBase.jungle.biomeID || k == BiomeGenBase.jungleHills.biomeID) && nextInt(8) == 0)
                    {
                        ai1[j + i * par3] = BiomeGenBase.river.biomeID;
                    }
                    else
                    {
                        ai1[j + i * par3] = k;
                    }
                }
            }

            return ai1;
        }
    }
}
