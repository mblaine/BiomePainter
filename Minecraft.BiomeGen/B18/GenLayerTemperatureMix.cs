
namespace Minecraft.BiomeGen.B18
{
    public class GenLayerTemperatureMix : GenLayer
    {
        private GenLayer field_35505_b;
        private int field_35506_c;

        public GenLayerTemperatureMix(GenLayer genlayer, GenLayer genlayer1, int i)
            : base(0L)
        {
            parent = genlayer1;
            field_35505_b = genlayer;
            field_35506_c = i;
        }

        public override int[] func_35500_a(int i, int j, int k, int l)
        {
            int[] ai = parent.func_35500_a(i, j, k, l);
            int[] ai1 = field_35505_b.func_35500_a(i, j, k, l);
            int[] ai2 = IntCache.getIntCache(k * l);
            for (int i1 = 0; i1 < k * l; i1++)
            {
                ai2[i1] = ai1[i1] + (BiomeGenBase.biomeList[ai[i1]].func_35474_f() - ai1[i1]) / (field_35506_c * 2 + 1);
            }
            return ai2;
        }
    }
}
