
namespace Minecraft.BiomeGen.F12
{
    public class GenLayerShore : GenLayer
    {
        public GenLayerShore(long par1, GenLayer par3GenLayer)
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

                    if (k == BiomeGenBase.mushroomIsland.biomeID)
                    {
                        int l = ai[j + 1 + ((i + 1) - 1) * (par3 + 2)];
                        int k1 = ai[j + 1 + 1 + (i + 1) * (par3 + 2)];
                        int j2 = ai[((j + 1) - 1) + (i + 1) * (par3 + 2)];
                        int i3 = ai[j + 1 + (i + 1 + 1) * (par3 + 2)];

                        if (l == BiomeGenBase.ocean.biomeID || k1 == BiomeGenBase.ocean.biomeID || j2 == BiomeGenBase.ocean.biomeID || i3 == BiomeGenBase.ocean.biomeID)
                        {
                            ai1[j + i * par3] = BiomeGenBase.mushroomIslandShore.biomeID;
                        }
                        else
                        {
                            ai1[j + i * par3] = k;
                        }

                        continue;
                    }

                    if (k != BiomeGenBase.ocean.biomeID && k != BiomeGenBase.river.biomeID && k != BiomeGenBase.swampland.biomeID && k != BiomeGenBase.extremeHills.biomeID)
                    {
                        int i1 = ai[j + 1 + ((i + 1) - 1) * (par3 + 2)];
                        int l1 = ai[j + 1 + 1 + (i + 1) * (par3 + 2)];
                        int k2 = ai[((j + 1) - 1) + (i + 1) * (par3 + 2)];
                        int j3 = ai[j + 1 + (i + 1 + 1) * (par3 + 2)];

                        if (i1 == BiomeGenBase.ocean.biomeID || l1 == BiomeGenBase.ocean.biomeID || k2 == BiomeGenBase.ocean.biomeID || j3 == BiomeGenBase.ocean.biomeID)
                        {
                            ai1[j + i * par3] = BiomeGenBase.beach.biomeID;
                        }
                        else
                        {
                            ai1[j + i * par3] = k;
                        }

                        continue;
                    }

                    if (k == BiomeGenBase.extremeHills.biomeID)
                    {
                        int j1 = ai[j + 1 + ((i + 1) - 1) * (par3 + 2)];
                        int i2 = ai[j + 1 + 1 + (i + 1) * (par3 + 2)];
                        int l2 = ai[((j + 1) - 1) + (i + 1) * (par3 + 2)];
                        int k3 = ai[j + 1 + (i + 1 + 1) * (par3 + 2)];

                        if (j1 != BiomeGenBase.extremeHills.biomeID || i2 != BiomeGenBase.extremeHills.biomeID || l2 != BiomeGenBase.extremeHills.biomeID || k3 != BiomeGenBase.extremeHills.biomeID)
                        {
                            ai1[j + i * par3] = BiomeGenBase.extremeHillsEdge.biomeID;
                        }
                        else
                        {
                            ai1[j + i * par3] = k;
                        }
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
