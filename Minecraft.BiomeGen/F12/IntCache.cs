using System.Collections;

namespace Minecraft.BiomeGen.F12
{
    public class IntCache
    {
        private static int intCacheSize = 256;

        /**
         * A ArrayList of pre-allocated int[256] arrays that are currently unused and can be returned by getIntCache()
         */
        private static ArrayList freeSmallArrays = new ArrayList();

        /**
         * A ArrayList of pre-allocated int[256] arrays that were previously returned by getIntCache() and which will not be re-
         * used again until resetIntCache() is called.
         */
        private static ArrayList inUseSmallArrays = new ArrayList();

        /**
         * A ArrayList of pre-allocated int[cacheSize] arrays that are currently unused and can be returned by getIntCache()
         */
        private static ArrayList freeLargeArrays = new ArrayList();

        /**
         * A ArrayList of pre-allocated int[cacheSize] arrays that were previously returned by getIntCache() and which will not
         * be re-used again until resetIntCache() is called.
         */
        private static ArrayList inUseLargeArrays = new ArrayList();

        public IntCache()
        {
        }

        public static int[] getIntCache(int par0)
        {
            if (par0 <= 256)
            {
                if (freeSmallArrays.Count == 0)
                {
                    int[] ai = new int[256];
                    inUseSmallArrays.Add(ai);
                    return ai;
                }
                else
                {
                    int[] ai1 = (int[])freeSmallArrays[freeSmallArrays.Count - 1];
                    freeSmallArrays.RemoveAt(freeSmallArrays.Count - 1);
                    inUseSmallArrays.Add(ai1);
                    return ai1;
                }
            }

            if (par0 > intCacheSize)
            {
                intCacheSize = par0;
                freeLargeArrays.Clear();
                inUseLargeArrays.Clear();
                int[] ai2 = new int[intCacheSize];
                inUseLargeArrays.Add(ai2);
                return ai2;
            }

            if (freeLargeArrays.Count == 0)
            {
                int[] ai3 = new int[intCacheSize];
                inUseLargeArrays.Add(ai3);
                return ai3;
            }
            else
            {
                int[] ai4 = (int[])freeLargeArrays[freeLargeArrays.Count - 1];
                freeLargeArrays.RemoveAt(freeLargeArrays.Count - 1);
                inUseLargeArrays.Add(ai4);
                return ai4;
            }
        }

        /**
         * Mark all pre-allocated arrays as available for re-use by moving them to the appropriate free ArrayLists.
         */
        public static void resetIntCache()
        {
            if (freeLargeArrays.Count > 0)
            {
                freeLargeArrays.RemoveAt(freeLargeArrays.Count - 1);
            }

            if (freeSmallArrays.Count > 0)
            {
                freeSmallArrays.RemoveAt(freeSmallArrays.Count - 1);
            }

            freeLargeArrays.AddRange(inUseLargeArrays);
            freeSmallArrays.AddRange(inUseSmallArrays);
            inUseLargeArrays.Clear();
            inUseSmallArrays.Clear();
        }
    }
}
