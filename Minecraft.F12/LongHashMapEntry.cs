using System;
using System.Text;

namespace Minecraft.F12
{
    public class LongHashMapEntry
    {
        /**
         * the key as a long (for playerInstances it is the x in the most significant 32 bits and then y)
         */
        public long key;

        /** the value held by the hash at the specified key */
        public Object value;

        /** the next hashentry in the table */
        public LongHashMapEntry nextEntry;
        public int hash;

        public LongHashMapEntry(int par1, long par2, Object par4Obj, LongHashMapEntry par5LongHashMapEntry)
        {
            value = par4Obj;
            nextEntry = par5LongHashMapEntry;
            key = par2;
            hash = par1;
        }

        public long getKey()
        {
            return key;
        }

        public Object getValue()
        {
            return value;
        }

        public bool equals(Object par1Obj)
        {
            if (!(par1Obj is LongHashMapEntry))
            {
                return false;
            }

            LongHashMapEntry longhashmapentry = (LongHashMapEntry)par1Obj;
            Int64? long1 = getKey();
            Int64? long2 = longhashmapentry.getKey();

            if (long1 == long2 || long1 != null && long1.Equals(long2))
            {
                Object obj = getValue();
                Object obj1 = longhashmapentry.getValue();

                if (obj == obj1 || obj != null && obj.Equals(obj1))
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
            return (new StringBuilder()).Append(getKey()).Append("=").Append(getValue()).ToString();
        }
    }
}
