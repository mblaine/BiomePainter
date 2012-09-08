using System;

namespace Minecraft.F10
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
            if (f > 0.1F && f < 0.2F)
            {
                throw new ArgumentException("Please avoid temperatures in the range 0.1 - 0.2 because of snow");
            }
            else
            {
                temperature = f;
                rainfall = f1;
                return this;
            }
        }

        public int getIntRainfall()
        {
            return (int)(rainfall * 65536F);
        }

        public int getIntTemperature()
        {
            return (int)(temperature * 65536F);
        }

        public static BiomeGenBase ocean = new BiomeGenBase(0);
        public static BiomeGenBase plains = new BiomeGenBase(1).setTemperatureRainfall(0.8F, 0.4F);
        public static BiomeGenBase desert = new BiomeGenBase(2).setTemperatureRainfall(2.0F, 0.0F);
        public static BiomeGenBase extremeHills = new BiomeGenBase(3).setTemperatureRainfall(0.2F, 0.3F);
        public static BiomeGenBase forest = new BiomeGenBase(4).setTemperatureRainfall(0.7F, 0.8F);
        public static BiomeGenBase taiga = new BiomeGenBase(5).setTemperatureRainfall(0.3F, 0.8F);
        public static BiomeGenBase swampland = new BiomeGenBase(6).setTemperatureRainfall(0.8F, 0.9F);
        public static BiomeGenBase river = new BiomeGenBase(7);
        public static BiomeGenBase hell = new BiomeGenBase(8).setTemperatureRainfall(2.0F, 0.0F);
        public static BiomeGenBase sky = new BiomeGenBase(9);
        public static BiomeGenBase frozenOcean = new BiomeGenBase(10).setTemperatureRainfall(0.0F, 0.5F);
        public static BiomeGenBase frozenRiver = new BiomeGenBase(11).setTemperatureRainfall(0.0F, 0.5F);
        public static BiomeGenBase icePlains = new BiomeGenBase(12).setTemperatureRainfall(0.0F, 0.5F);
        public static BiomeGenBase iceMountains = new BiomeGenBase(13).setTemperatureRainfall(0.0F, 0.5F);
        public static BiomeGenBase mushroomIsland = new BiomeGenBase(14).setTemperatureRainfall(0.9F, 1.0F);
        public static BiomeGenBase mushroomIslandShore = new BiomeGenBase(15).setTemperatureRainfall(0.9F, 1.0F);

    }
}
