
namespace Minecraft.BiomeGen.F12
{
    public class GenLayerIsland : GenLayer
    {
        public GenLayerIsland(long par1)
            : base(par1)
        {
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int[] ai = IntCache.getIntCache(par3 * par4);

            for (int i = 0; i < par4; i++)
            {
                for (int j = 0; j < par3; j++)
                {
                    initChunkSeed(par1 + j, par2 + i);
                    ai[j + i * par3] = nextInt(10) != 0 ? 0 : 1;
                }
            }

            if (par1 > -par3 && par1 <= 0 && par2 > -par4 && par2 <= 0)
            {
                ai[-par1 + -par2 * par3] = 1;
            }

            return ai;
        }
    }
}
