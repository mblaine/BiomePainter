
namespace Minecraft.F12
{
    public class GenLayerRiverMix : GenLayer
    {
        private GenLayer field_35512_b;
        private GenLayer field_35513_c;

        public GenLayerRiverMix(long par1, GenLayer par3GenLayer, GenLayer par4GenLayer)
            : base(par1)
        {
            field_35512_b = par3GenLayer;
            field_35513_c = par4GenLayer;
        }

        /**
         * Initialize layer's local worldGenSeed based on its own baseSeed and the world's global seed (passed in as an
         * argument).
         */
        public override void initWorldGenSeed(long par1)
        {
            field_35512_b.initWorldGenSeed(par1);
            field_35513_c.initWorldGenSeed(par1);
            base.initWorldGenSeed(par1);
        }

        public override int[] getInts(int par1, int par2, int par3, int par4)
        {
            int[] ai = field_35512_b.getInts(par1, par2, par3, par4);
            int[] ai1 = field_35513_c.getInts(par1, par2, par3, par4);
            int[] ai2 = IntCache.getIntCache(par3 * par4);

            for (int i = 0; i < par3 * par4; i++)
            {
                if (ai[i] == BiomeGenBase.ocean.biomeID)
                {
                    ai2[i] = ai[i];
                    continue;
                }

                if (ai1[i] >= 0)
                {
                    if (ai[i] == BiomeGenBase.icePlains.biomeID)
                    {
                        ai2[i] = BiomeGenBase.frozenRiver.biomeID;
                        continue;
                    }

                    if (ai[i] == BiomeGenBase.mushroomIsland.biomeID || ai[i] == BiomeGenBase.mushroomIslandShore.biomeID)
                    {
                        ai2[i] = BiomeGenBase.mushroomIslandShore.biomeID;
                    }
                    else
                    {
                        ai2[i] = ai1[i];
                    }
                }
                else
                {
                    ai2[i] = ai[i];
                }
            }

            return ai2;
        }
    }
}
