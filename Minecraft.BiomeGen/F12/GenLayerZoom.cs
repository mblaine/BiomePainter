using System;

namespace Minecraft.F12
{
    public class GenLayerZoom : GenLayer
    {
        public GenLayerZoom(long par1, GenLayer par3GenLayer)
            : base(par1)
        {
            base.parent = par3GenLayer;
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int i = par1 >> 1;
            int j = par2 >> 1;
            int k = (par3 >> 1) + 3;
            int l = (par4 >> 1) + 3;
            int[] ai = parent.getInts(i, j, k, l);
            int[] ai1 = IntCache.getIntCache(k * 2 * (l * 2));
            int i1 = k << 1;

            for (int j1 = 0; j1 < l - 1; j1++)
            {
                int k1 = j1 << 1;
                int i2 = k1 * i1;
                int j2 = ai[0 + (j1 + 0) * k];
                int k2 = ai[0 + (j1 + 1) * k];

                for (int l2 = 0; l2 < k - 1; l2++)
                {
                    initChunkSeed(l2 + i << 1, j1 + j << 1);
                    int i3 = ai[l2 + 1 + (j1 + 0) * k];
                    int j3 = ai[l2 + 1 + (j1 + 1) * k];
                    ai1[i2] = j2;
                    ai1[i2++ + i1] = choose(j2, k2);
                    ai1[i2] = choose(j2, i3);
                    ai1[i2++ + i1] = func_35514_b(j2, i3, k2, j3);
                    j2 = i3;
                    k2 = j3;
                }
            }

            int[] ai2 = IntCache.getIntCache(par3 * par4);

            for (int l1 = 0; l1 < par4; l1++)
            {
                Array.Copy(ai1, (l1 + (par2 & 1)) * (k << 1) + (par1 & 1), ai2, l1 * par3, par3);
            }

            return ai2;
        }

        protected int choose(int par1, int par2)
        {
            return nextInt(2) != 0 ? par2 : par1;
        }

        protected int func_35514_b(int par1, int par2, int par3, int par4)
        {
            if (par2 == par3 && par3 == par4)
            {
                return par2;
            }

            if (par1 == par2 && par1 == par3)
            {
                return par1;
            }

            if (par1 == par2 && par1 == par4)
            {
                return par1;
            }

            if (par1 == par3 && par1 == par4)
            {
                return par1;
            }

            if (par1 == par2 && par3 != par4)
            {
                return par1;
            }

            if (par1 == par3 && par2 != par4)
            {
                return par1;
            }

            if (par1 == par4 && par2 != par3)
            {
                return par1;
            }

            if (par2 == par1 && par3 != par4)
            {
                return par2;
            }

            if (par2 == par3 && par1 != par4)
            {
                return par2;
            }

            if (par2 == par4 && par1 != par3)
            {
                return par2;
            }

            if (par3 == par1 && par2 != par4)
            {
                return par3;
            }

            if (par3 == par2 && par1 != par4)
            {
                return par3;
            }

            if (par3 == par4 && par1 != par2)
            {
                return par3;
            }

            if (par4 == par1 && par2 != par3)
            {
                return par3;
            }

            if (par4 == par2 && par1 != par3)
            {
                return par3;
            }

            if (par4 == par3 && par1 != par2)
            {
                return par3;
            }

            int i = nextInt(4);

            if (i == 0)
            {
                return par1;
            }

            if (i == 1)
            {
                return par2;
            }

            if (i == 2)
            {
                return par3;
            }
            else
            {
                return par4;
            }
        }

        public static GenLayer func_35515_a(long par0, GenLayer par2GenLayer, int par3)
        {
            Object obj = par2GenLayer;

            for (int i = 0; i < par3; i++)
            {
                obj = new GenLayerZoom(par0 + (long)i, ((GenLayer)(obj)));
            }

            return ((GenLayer)(obj));
        }
    }
}
