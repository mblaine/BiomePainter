using System;

namespace Minecraft
{
    //http://docs.oracle.com/javase/6/docs/api/java/util/Random.html
    //http://stackoverflow.com/questions/2147524/c-java-number-randomization
    public class JavaRandom
    {
        private Int64 seed;
        private bool haveNextNextGaussian;
        private double nextNextGaussian;

        public JavaRandom()
        {
            setSeed(Convert.ToInt64(((TimeSpan)(DateTime.UtcNow - new DateTime(1970, 1, 1))).TotalMilliseconds));
        }

        public JavaRandom(Int64 s)
        {
            setSeed(s);
        }


        protected Int32 next(Int32 bits)
        {
            seed = (seed * 0x5DEECE66DL + 0xBL) & ((1L << 48) - 1);
            return (Int32)(seed >> (48 - bits));
        }

        public bool nextBoolean()
        {
            return next(1) != 0;
        }

        public void nextBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; )
                for (int rnd = nextInt(), n = Math.Min(bytes.Length - i, 4); n-- > 0; rnd >>= 8)
                    bytes[i++] = (byte)rnd;
        }

        public double nextDouble()
        {
            return (((Int64)next(26) << 27) + next(27)) / (double)(1L << 53);
        }

        public float nextFloat()
        {
            return next(24) / ((float)(1 << 24));
        }

        public double nextGaussian()
        {
            if (haveNextNextGaussian)
            {
                haveNextNextGaussian = false;
                return nextNextGaussian;
            }
            else
            {
                double v1, v2, s;
                do
                {
                    v1 = 2 * nextDouble() - 1;   // between -1.0 and 1.0
                    v2 = 2 * nextDouble() - 1;   // between -1.0 and 1.0
                    s = v1 * v1 + v2 * v2;
                } while (s >= 1 || s == 0);
                double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
                nextNextGaussian = v2 * multiplier;
                haveNextNextGaussian = true;
                return v1 * multiplier;
            }
        }

        public Int32 nextInt()
        {
            return next(32);
        }

        public Int32 nextInt(Int32 n)
        {
            if (n <= 0)
                throw new ArgumentException("n must be positive");

            if ((n & -n) == n)  // i.e., n is a power of 2
                return (int)((n * (Int64)next(31)) >> 31);

            int bits, val;
            do
            {
                bits = next(31);
                val = bits % n;
            } while (bits - val + (n - 1) < 0);
            return val;
        }

        public Int64 nextLong()
        {
            return ((Int64)next(32) << 32) + next(32);
        }

        public void setSeed(Int64 s)
        {
            seed = (s ^ 0x5DEECE66DL) & ((1L << 48) - 1);
            haveNextNextGaussian = false;
        }
    }
}