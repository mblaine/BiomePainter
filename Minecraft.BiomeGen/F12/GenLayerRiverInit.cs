
namespace Minecraft.F12
{
    public class GenLayerRiverInit : GenLayer
    {
        public GenLayerRiverInit(long par1, GenLayer par3GenLayer)
            : base(par1)
        {
            parent = par3GenLayer;
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int[] ai = parent.getInts(par1, par2, par3, par4);
            int[] ai1 = IntCache.getIntCache(par3 * par4);

            for (int i = 0; i < par4; i++)
            {
                for (int j = 0; j < par3; j++)
                {
                    initChunkSeed(j + par1, i + par2);
                    ai1[j + i * par3] = ai[j + i * par3] <= 0 ? 0 : nextInt(2) + 2;
                }
            }

            return ai1;
        }
    }
}
