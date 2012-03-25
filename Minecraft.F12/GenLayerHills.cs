﻿
namespace Minecraft.F12
{
    public class GenLayerHills : GenLayer
    {
        public GenLayerHills(long par1, GenLayer par3GenLayer)
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

                    if (nextInt(3) == 0)
                    {
                        int l = k;

                        if (k == BiomeGenBase.desert.biomeID)
                        {
                            l = BiomeGenBase.desertHills.biomeID;
                        }
                        else if (k == BiomeGenBase.forest.biomeID)
                        {
                            l = BiomeGenBase.forestHills.biomeID;
                        }
                        else if (k == BiomeGenBase.taiga.biomeID)
                        {
                            l = BiomeGenBase.taigaHills.biomeID;
                        }
                        else if (k == BiomeGenBase.plains.biomeID)
                        {
                            l = BiomeGenBase.forest.biomeID;
                        }
                        else if (k == BiomeGenBase.icePlains.biomeID)
                        {
                            l = BiomeGenBase.iceMountains.biomeID;
                        }
                        else if (k == BiomeGenBase.jungle.biomeID)
                        {
                            l = BiomeGenBase.jungleHills.biomeID;
                        }

                        if (l != k)
                        {
                            int i1 = ai[j + 1 + ((i + 1) - 1) * (par3 + 2)];
                            int j1 = ai[j + 1 + 1 + (i + 1) * (par3 + 2)];
                            int k1 = ai[((j + 1) - 1) + (i + 1) * (par3 + 2)];
                            int l1 = ai[j + 1 + (i + 1 + 1) * (par3 + 2)];

                            if (i1 == k && j1 == k && k1 == k && l1 == k)
                            {
                                ai1[j + i * par3] = l;
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
