using System;

namespace Minecraft.F12
{
    public class GenLayerVoronoiZoom : GenLayer
    {
        public GenLayerVoronoiZoom(long par1, GenLayer par3GenLayer)
            : base(par1)
        {
            base.parent = par3GenLayer;
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            par1 -= 2;
            par2 -= 2;
            byte byte0 = 2;
            int i = 1 << byte0;
            int j = par1 >> byte0;
            int k = par2 >> byte0;
            int l = (par3 >> byte0) + 3;
            int i1 = (par4 >> byte0) + 3;
            int[] ai = parent.getInts(j, k, l, i1);
            int j1 = l << byte0;
            int k1 = i1 << byte0;
            int[] ai1 = IntCache.getIntCache(j1 * k1);

            for (int l1 = 0; l1 < i1 - 1; l1++)
            {
                int i2 = ai[0 + (l1 + 0) * l];
                int k2 = ai[0 + (l1 + 1) * l];

                for (int l2 = 0; l2 < l - 1; l2++)
                {
                    double d = (double)i * 0.90000000000000002D;
                    initChunkSeed(l2 + j << byte0, l1 + k << byte0);
                    double d1 = ((double)nextInt(1024) / 1024D - 0.5D) * d;
                    double d2 = ((double)nextInt(1024) / 1024D - 0.5D) * d;
                    initChunkSeed(l2 + j + 1 << byte0, l1 + k << byte0);
                    double d3 = ((double)nextInt(1024) / 1024D - 0.5D) * d + (double)i;
                    double d4 = ((double)nextInt(1024) / 1024D - 0.5D) * d;
                    initChunkSeed(l2 + j << byte0, l1 + k + 1 << byte0);
                    double d5 = ((double)nextInt(1024) / 1024D - 0.5D) * d;
                    double d6 = ((double)nextInt(1024) / 1024D - 0.5D) * d + (double)i;
                    initChunkSeed(l2 + j + 1 << byte0, l1 + k + 1 << byte0);
                    double d7 = ((double)nextInt(1024) / 1024D - 0.5D) * d + (double)i;
                    double d8 = ((double)nextInt(1024) / 1024D - 0.5D) * d + (double)i;
                    int i3 = ai[l2 + 1 + (l1 + 0) * l];
                    int j3 = ai[l2 + 1 + (l1 + 1) * l];

                    for (int k3 = 0; k3 < i; k3++)
                    {
                        int l3 = ((l1 << byte0) + k3) * j1 + (l2 << byte0);

                        for (int i4 = 0; i4 < i; i4++)
                        {
                            double d9 = ((double)k3 - d2) * ((double)k3 - d2) + ((double)i4 - d1) * ((double)i4 - d1);
                            double d10 = ((double)k3 - d4) * ((double)k3 - d4) + ((double)i4 - d3) * ((double)i4 - d3);
                            double d11 = ((double)k3 - d6) * ((double)k3 - d6) + ((double)i4 - d5) * ((double)i4 - d5);
                            double d12 = ((double)k3 - d8) * ((double)k3 - d8) + ((double)i4 - d7) * ((double)i4 - d7);

                            if (d9 < d10 && d9 < d11 && d9 < d12)
                            {
                                ai1[l3++] = i2;
                                continue;
                            }

                            if (d10 < d9 && d10 < d11 && d10 < d12)
                            {
                                ai1[l3++] = i3;
                                continue;
                            }

                            if (d11 < d9 && d11 < d10 && d11 < d12)
                            {
                                ai1[l3++] = k2;
                            }
                            else
                            {
                                ai1[l3++] = j3;
                            }
                        }
                    }

                    i2 = i3;
                    k2 = j3;
                }
            }

            int[] ai2 = IntCache.getIntCache(par3 * par4);

            for (int j2 = 0; j2 < par4; j2++)
            {
                Array.Copy(ai1, (j2 + (par2 & i - 1)) * (l << byte0) + (par1 & i - 1), ai2, j2 * par3, par3);
            }

            return ai2;
        }
    }
}
