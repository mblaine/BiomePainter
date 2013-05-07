namespace Minecraft.BiomeGen.F15
{
    public class GenLayerRiverMix : GenLayer
    {
        private GenLayer biomePatternGeneratorChain;
        private GenLayer riverPatternGeneratorChain;

        public GenLayerRiverMix(long par1, GenLayer par3GenLayer, GenLayer par4GenLayer)
            : base(par1)
        {
            this.biomePatternGeneratorChain = par3GenLayer;
            this.riverPatternGeneratorChain = par4GenLayer;
        }

        public override void initWorldGenSeed(long par1)
        {
            this.biomePatternGeneratorChain.initWorldGenSeed(par1);
            this.riverPatternGeneratorChain.initWorldGenSeed(par1);
            base.initWorldGenSeed(par1);
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int[] var5 = this.biomePatternGeneratorChain.getInts(par1, par2, par3, par4);
            int[] var6 = this.riverPatternGeneratorChain.getInts(par1, par2, par3, par4);
            int[] var7 = IntCache.getIntCache(par3 * par4);

            for (int var8 = 0; var8 < par3 * par4; ++var8)
            {
                if (var5[var8] == BiomeGenBase.ocean.biomeID)
                {
                    var7[var8] = var5[var8];
                }
                else if (var6[var8] >= 0)
                {
                    if (var5[var8] == BiomeGenBase.icePlains.biomeID)
                    {
                        var7[var8] = BiomeGenBase.frozenRiver.biomeID;
                    }
                    else if (var5[var8] != BiomeGenBase.mushroomIsland.biomeID && var5[var8] != BiomeGenBase.mushroomIslandShore.biomeID)
                    {
                        var7[var8] = var6[var8];
                    }
                    else
                    {
                        var7[var8] = BiomeGenBase.mushroomIslandShore.biomeID;
                    }
                }
                else
                {
                    var7[var8] = var5[var8];
                }
            }

            return var7;
        }
    }
}