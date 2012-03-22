using System;
using System.Collections;

namespace Minecraft.F11
{
    public class IntCache
    {
        private static int intCacheSize = 256;
        private static ArrayList freeSmallArrays = new ArrayList();
        private static ArrayList inUseSmallArrays = new ArrayList();
        private static ArrayList freeLargeArrays = new ArrayList();
        private static ArrayList inUseLargeArrays = new ArrayList();

        public IntCache()
        {
        }

        public static int[] getIntCache(int i)
        {
            if (i <= 256)
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
            if (i > intCacheSize)
            {
                intCacheSize = i;
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
