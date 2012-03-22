using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minecraft.B18
{
    public class GenLayerDownfall : GenLayer
    {
        public GenLayerDownfall(GenLayer genlayer)
            : base(0L)
        {
            parent = genlayer;
        }

        public override int[] func_35500_a(int i, int j, int k, int l)
        {
            int[] ai = parent.func_35500_a(i, j, k, l);
            int[] ai1 = IntCache.getIntCache(k * l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                ai1[i1] = BiomeGenBase.biomeList[ai[i1]].func_35476_e();
            }

            return ai1;
        }
    }
}
