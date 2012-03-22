using System;

namespace Minecraft.F11
{
    public class GenLayerTemperature : GenLayer
    {
        public GenLayerTemperature(GenLayer genlayer)
            : base(0L)
        {
            parent = genlayer;
        }

        public override int[] getInts(int i, int j, int k, int l)
        {
            int[] ai = parent.getInts(i, j, k, l);
            int[] ai1 = IntCache.getIntCache(k * l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                ai1[i1] = BiomeGenBase.biomeList[ai[i1]].getIntTemperature();
            }

            return ai1;
        }
    }
}
