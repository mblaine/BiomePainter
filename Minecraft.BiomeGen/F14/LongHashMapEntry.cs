using System;

namespace Minecraft.BiomeGen.F14
{
    class LongHashMapEntry
    {
        public long key;

        public Object value;

        public LongHashMapEntry nextEntry;
        public int hash;

        public LongHashMapEntry(int par1, long par2, Object par4Obj, LongHashMapEntry par5LongHashMapEntry)
        {
            this.value = par4Obj;
            this.nextEntry = par5LongHashMapEntry;
            this.key = par2;
            this.hash = par1;
        }

        public long getKey()
        {
            return this.key;
        }

        public Object getValue()
        {
            return this.value;
        }

        public bool equals(Object par1Obj)
        {
            if (!(par1Obj is LongHashMapEntry))
            {
                return false;
            }
            else
            {
                LongHashMapEntry var2 = (LongHashMapEntry)par1Obj;
                Int64? var3 = this.getKey();
                Int64? var4 = var2.getKey();

                if (var3 == var4 || var3 != null && var3.Equals(var4))
                {
                    Object var5 = this.getValue();
                    Object var6 = var2.getValue();

                    if (var5 == var6 || var5 != null && var5.Equals(var6))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public int hashCode()
        {
            return LongHashMap.getHashCode(this.key);
        }

        public String toString()
        {
            return this.getKey() + "=" + this.getValue();
        }
    }
}