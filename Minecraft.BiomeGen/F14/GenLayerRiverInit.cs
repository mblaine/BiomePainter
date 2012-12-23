namespace Minecraft.BiomeGen.F14
{
    public class GenLayerRiverInit : GenLayer
    {
        public GenLayerRiverInit(long par1, GenLayer par3GenLayer)
            : base(par1)
        {
            this.parent = par3GenLayer;
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int[] var5 = this.parent.getInts(par1, par2, par3, par4);
            int[] var6 = IntCache.getIntCache(par3 * par4);

            for (int var7 = 0; var7 < par4; ++var7)
            {
                for (int var8 = 0; var8 < par3; ++var8)
                {
                    this.initChunkSeed((long)(var8 + par1), (long)(var7 + par2));
                    var6[var8 + var7 * par3] = var5[var8 + var7 * par3] > 0 ? this.nextInt(2) + 2 : 0;
                }
            }

            return var6;
        }
    }
}