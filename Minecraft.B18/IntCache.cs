using System;
using System.Collections;

namespace Minecraft.B18
{
    public class IntCache
    {
        private static int intCacheSize = 256;
        private static ArrayList field_35271_b = new ArrayList();
        private static ArrayList field_35272_c = new ArrayList();
        private static ArrayList field_35269_d = new ArrayList();
        private static ArrayList field_35270_e = new ArrayList();

        public IntCache()
        {
        }

        public static int[] getIntCache(int i)
        {
            if (i <= 256)
            {
                if (field_35271_b.Count == 0)
                {
                    int[] ai = new int[256];
                    field_35272_c.Add(ai);
                    return ai;
                }
                else
                {
                    int[] ai1 = (int[])field_35271_b[field_35271_b.Count - 1];
                    field_35271_b.RemoveAt(field_35271_b.Count - 1);
                    field_35272_c.Add(ai1);
                    return ai1;
                }
            }
            if (i > intCacheSize)
            {
                //System.out.println((new StringBuilder()).append("New max size: ").append(i).toString());
                intCacheSize = i;
                field_35269_d.Clear();
                field_35270_e.Clear();
                int[] ai2 = new int[intCacheSize];
                field_35270_e.Add(ai2);
                return ai2;
            }
            if (field_35269_d.Count == 0)
            {
                int[] ai3 = new int[intCacheSize];
                field_35270_e.Add(ai3);
                return ai3;
            }
            else
            {
                int[] ai4 = (int[])field_35269_d[field_35269_d.Count - 1];
                field_35269_d.RemoveAt(field_35269_d.Count - 1);
                field_35270_e.Add(ai4);
                return ai4;
            }
        }

        public static void func_35268_a()
        {
            if (field_35269_d.Count > 0)
            {
                field_35269_d.RemoveAt(field_35269_d.Count - 1);
            }
            if (field_35271_b.Count > 0)
            {
                field_35271_b.RemoveAt(field_35271_b.Count - 1);
            }
            field_35269_d.AddRange(field_35270_e);
            field_35271_b.AddRange(field_35272_c);
            field_35270_e.Clear();
            field_35272_c.Clear();
        }
    }
}
