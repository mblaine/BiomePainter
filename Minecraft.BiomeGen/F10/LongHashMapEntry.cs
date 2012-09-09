using System;
using System.Text;

namespace Minecraft.BiomeGen.F10
{
    public class LongHashMapEntry
    {
        public long key;
        public Object value;
        public LongHashMapEntry nextEntry;
        public int field_35831_d;

        public LongHashMapEntry(int i, long l, Object obj, LongHashMapEntry longhashmapentry)
        {
            value = obj;
            nextEntry = longhashmapentry;
            key = l;
            field_35831_d = i;
        }

        public long func_35830_a()
        {
            return key;
        }

        public Object func_35829_b()
        {
            return value;
        }

        public bool equals(Object obj)
        {
            if (!(obj is LongHashMapEntry))
            {
                return false;
            }
            LongHashMapEntry longhashmapentry = (LongHashMapEntry)obj;
            Int64? long1 = func_35830_a();
            Int64? long2 = longhashmapentry.func_35830_a();
            if (long1 == long2 || long1 != null && long1.Equals(long2))
            {
                Object obj1 = func_35829_b();
                Object obj2 = longhashmapentry.func_35829_b();
                if (obj1 == obj2 || obj1 != null && obj1.Equals(obj2))
                {
                    return true;
                }
            }
            return false;
        }

        public int hashCode()
        {
            return LongHashMap.getHashCode(key);
        }

        public override String ToString()
        {
            return (new StringBuilder()).Append(func_35830_a()).Append("=").Append(func_35829_b()).ToString();
        }
    }
}
