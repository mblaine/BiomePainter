
namespace Minecraft.BiomeGen.B13
{
    public class MobSpawnerBase : Minecraft.BiomeUtil
    {
        private WorldChunkManager world;


        public MobSpawnerBase(long seed)
            : base(seed)
        {
            world = new WorldChunkManager(Seed);
        }

        public override Biome GetBiome(int x, int z)
        {
            float temp = (float)world.getTemperature(x, z);
            float humid = (float)world.getHumidity(x, z);

            humid *= temp;

            if (temp < 0.1F)
            {
                //Tundra
                return Biome.IcePlains;
            }
            if (humid < 0.2F)
            {
                if (temp < 0.5F)
                {
                    //Tundra
                    return Biome.IcePlains;
                }
                if (temp < 0.95F)
                {
                    //Savanna
                    return Biome.Plains;
                }
                else
                {
                    return Biome.Desert;
                }
            }
            if (humid > 0.5F && temp < 0.7F)
            {
                //Swampland
                return Biome.Forest;
            }
            if (temp < 0.5F)
            {
                return Biome.Taiga;
            }
            if (temp < 0.97F)
            {
                if (humid < 0.35F)
                {
                    //Shrubland
                    return Biome.Forest;
                }
                else
                {
                    return Biome.Forest;
                }
            }
            if (humid < 0.45F)
            {
                return Biome.Plains;
            }
            if (humid < 0.9F)
            {
                //Seasonal Forest
                return Biome.Forest;
            }
            else
            {
                //Rainforest
                return Biome.Jungle;
            }
        }
    }
}
