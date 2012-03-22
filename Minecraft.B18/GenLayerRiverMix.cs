using System;

namespace Minecraft.B18
{
    public class GenLayerRiverMix : GenLayer
    {
        private GenLayer field_35512_b;
        private GenLayer field_35513_c;

        public GenLayerRiverMix(long l, GenLayer genlayer, GenLayer genlayer1)
            : base(l)
        {
            field_35512_b = genlayer;
            field_35513_c = genlayer1;
        }

        public override void func_35496_b(long l)
        {
            field_35512_b.func_35496_b(l);
            field_35513_c.func_35496_b(l);
            base.func_35496_b(l);
        }

        public override int[] func_35500_a(int i, int j, int k, int l)
        {
            int[] ai = field_35512_b.func_35500_a(i, j, k, l);
            int[] ai1 = field_35513_c.func_35500_a(i, j, k, l);
            int[] ai2 = IntCache.getIntCache(k * l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                if (ai[i1] == BiomeGenBase.ocean.biomeID)
                {
                    ai2[i1] = ai[i1];
                }
                else
                {
                    ai2[i1] = ai1[i1] < 0 ? ai[i1] : ai1[i1];
                }
            }

            return ai2;
        }
    }
}
