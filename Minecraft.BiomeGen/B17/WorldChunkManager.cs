using System;

namespace Minecraft.BiomeGen.B17
{
    public class WorldChunkManager
    {
        public double[] temperature;
        public double[] humidity;
        public double[] field_4196_c;

        private NoiseGeneratorOctaves2 field_4194_e;
        private NoiseGeneratorOctaves2 field_4193_f;
        private NoiseGeneratorOctaves2 field_4192_g;

        public WorldChunkManager(Int64 seed)
        {
            field_4194_e = new NoiseGeneratorOctaves2(new JavaRandom(seed * 9871L), 4);
            field_4193_f = new NoiseGeneratorOctaves2(new JavaRandom(seed * 39811L), 4);
            field_4192_g = new NoiseGeneratorOctaves2(new JavaRandom(seed * 0x84a59L), 2);
        }

        public double getTemperature(int i, int j)
        {
            //temperature = field_4194_e.func_4112_a(temperature, i, j, 1, 1, 0.02500000037252903D, 0.02500000037252903D, 0.5D);
            temperature = getTemperatures(temperature, i, j, 1, 1);
            return temperature[0];
        }

        public double[] getTemperatures(double[] ad, int i, int j, int k, int l)
        {
            if (ad == null || ad.Length < k * l)
            {
                ad = new double[k * l];
            }
            ad = field_4194_e.func_4112_a(ad, i, j, k, l, 0.02500000037252903D, 0.02500000037252903D, 0.25D);
            field_4196_c = field_4192_g.func_4112_a(field_4196_c, i, j, k, l, 0.25D, 0.25D, 0.58823529411764708D);
            int i1 = 0;
            for (int j1 = 0; j1 < k; j1++)
            {
                for (int k1 = 0; k1 < l; k1++)
                {
                    double d = field_4196_c[i1] * 1.1000000000000001D + 0.5D;
                    double d1 = 0.01D;
                    double d2 = 1.0D - d1;
                    double d3 = (ad[i1] * 0.14999999999999999D + 0.69999999999999996D) * d2 + d * d1;
                    d3 = 1.0D - (1.0D - d3) * (1.0D - d3);
                    if (d3 < 0.0D)
                    {
                        d3 = 0.0D;
                    }
                    if (d3 > 1.0D)
                    {
                        d3 = 1.0D;
                    }
                    ad[i1] = d3;
                    i1++;
                }

            }

            return ad;
        }

        public double getHumidity(int i, int j)
        {
            humidity = getHumidities(humidity, i, j, 1, 1);
            return humidity[0];
        }

        //from BiomeGenBase[] loadBlockGeneratorData(BiomeGenBase[], int, int, int, int)
        public double[] getHumidities(double[] ad, int i, int j, int k, int l)
        {
            if (ad == null || ad.Length < k * l)
            {
                ad = new double[k * l];
            }
            ad = field_4193_f.func_4112_a(ad, i, j, k, k, 0.05000000074505806D, 0.05000000074505806D, 0.33333333333333331D);
            field_4196_c = field_4192_g.func_4112_a(field_4196_c, i, j, k, k, 0.25D, 0.25D, 0.58823529411764708D);
            int i1 = 0;
            for (int j1 = 0; j1 < k; j1++)
            {
                for (int k1 = 0; k1 < l; k1++)
                {
                    double d = field_4196_c[i1] * 1.1000000000000001D + 0.5D;
                    double d1 = 0.002D;
                    double d2 = 1.0D - d1;
                    double d4 = (ad[i1] * 0.14999999999999999D + 0.5D) * d2 + d * d1;
                    if (d4 < 0.0D)
                    {
                        d4 = 0.0D;
                    }
                    if (d4 > 1.0D)
                    {
                        d4 = 1.0D;
                    }
                    ad[i1++] = d4;
                }
            }
            return ad;
        }
    }
}