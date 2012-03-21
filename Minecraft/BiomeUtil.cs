namespace Minecraft
{
    public abstract class BiomeUtil
    {
        public long Seed;
        
        public BiomeUtil(long seed)
        {
            this.Seed = seed;
        }

        public abstract Biome GetBiome(int x, int z);
    }
}
