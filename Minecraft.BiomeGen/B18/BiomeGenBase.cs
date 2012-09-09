
namespace Minecraft.BiomeGen.B18
{
    public class BiomeGenBase
    {
        public int biomeID;
        public float temperature;
        public float rainfall;
        public static BiomeGenBase[] biomeList = new BiomeGenBase[256];

        protected BiomeGenBase(int i)
        {
            temperature = 0.5F;
            rainfall = 0.5F;
            biomeID = i;
            biomeList[i] = this;
        }

        private BiomeGenBase setTemperatureRainfall(float f, float f1)
        {
            temperature = f;
            rainfall = f1;
            return this;
        }

        public int func_35474_f()
        {
            return (int)(temperature * 65536F);
        }

        public int func_35476_e()
        {
            return (int)(rainfall * 65536F);
        }

        public static BiomeGenBase ocean = new BiomeGenBase(0);
        public static BiomeGenBase plains = new BiomeGenBase(1).setTemperatureRainfall(0.8F, 0.4F);
        public static BiomeGenBase desert = new BiomeGenBase(2).setTemperatureRainfall(2.0F, 0.0F);
        public static BiomeGenBase hills = new BiomeGenBase(3).setTemperatureRainfall(0.2F, 0.3F);
        public static BiomeGenBase forest = new BiomeGenBase(4).setTemperatureRainfall(0.7F, 0.8F);
        public static BiomeGenBase taiga = new BiomeGenBase(5).setTemperatureRainfall(0.3F, 0.8F);
        public static BiomeGenBase swampland = new BiomeGenBase(6).setTemperatureRainfall(0.8F, 0.9F);
        public static BiomeGenBase river = new BiomeGenBase(7);
        public static BiomeGenBase hell = new BiomeGenBase(8);
        public static BiomeGenBase sky = new BiomeGenBase(9);
    }
}
