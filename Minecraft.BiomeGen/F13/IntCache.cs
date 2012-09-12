using System.Collections;

namespace Minecraft.BiomeGen.F13
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

    public static int[] getIntCache(int par0)
    {
        int[] var1;

        if (par0 <= 256)
        {
            if (freeSmallArrays.Count == 0)
            {
                var1 = new int[256];
                inUseSmallArrays.Add(var1);
                return var1;
            }
            else
            {
                var1 = (int[])freeSmallArrays[freeSmallArrays.Count - 1];
                freeSmallArrays.RemoveAt(freeSmallArrays.Count - 1);
                inUseSmallArrays.Add(var1);
                return var1;
            }
        }
        else if (par0 > intCacheSize)
        {
            intCacheSize = par0;
            freeLargeArrays.Clear();
            inUseLargeArrays.Clear();
            var1 = new int[intCacheSize];
            inUseLargeArrays.Add(var1);
            return var1;
        }
        else if (freeLargeArrays.Count == 0)
        {
            var1 = new int[intCacheSize];
            inUseLargeArrays.Add(var1);
            return var1;
        }
        else
        {
            var1 = (int[])freeLargeArrays[freeLargeArrays.Count - 1];
            freeLargeArrays.RemoveAt(freeLargeArrays.Count - 1);
            inUseLargeArrays.Add(var1);
            return var1;
        }
    }

    /**
     * Mark all pre-allocated arrays as available for re-use by moving them to the appropriate free ArrayLists.
     */
    public static void resetIntCache()
    {
        if (!(freeLargeArrays.Count == 0))
        {
            freeLargeArrays.RemoveAt(freeLargeArrays.Count - 1);
        }

        if (!(freeSmallArrays.Count == 0))
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
