using System;

namespace Minecraft.BiomeGen.F14
{
    public class LongHashMap
    {
        private LongHashMapEntry[] hashArray = new LongHashMapEntry[16];

        private int numHashElements;

        private int capacity = 12;

        private float percentUseable = 0.75F;

        private volatile int modCount;

        private static int getHashedKey(long par0)
        {
            return hash((int)(par0 ^ par0 >> 32));
        }

        private static int hash(int par0)
        {
            par0 ^= par0 >> 20 ^ par0 >> 12;
            return par0 ^ par0 >> 7 ^ par0 >> 4;
        }

        private static int getHashIndex(int par0, int par1)
        {
            return par0 & par1 - 1;
        }

        public int getNumHashElements()
        {
            return this.numHashElements;
        }

        public Object getValueByKey(long par1)
        {
            int var3 = getHashedKey(par1);

            for (LongHashMapEntry var4 = this.hashArray[getHashIndex(var3, this.hashArray.Length)]; var4 != null; var4 = var4.nextEntry)
            {
                if (var4.key == par1)
                {
                    return var4.value;
                }
            }

            return null;
        }

        public bool containsItem(long par1)
        {
            return this.getEntry(par1) != null;
        }

        LongHashMapEntry getEntry(long par1)
        {
            int var3 = getHashedKey(par1);

            for (LongHashMapEntry var4 = this.hashArray[getHashIndex(var3, this.hashArray.Length)]; var4 != null; var4 = var4.nextEntry)
            {
                if (var4.key == par1)
                {
                    return var4;
                }
            }

            return null;
        }

        public void add(long par1, Object par3Obj)
        {
            int var4 = getHashedKey(par1);
            int var5 = getHashIndex(var4, this.hashArray.Length);

            for (LongHashMapEntry var6 = this.hashArray[var5]; var6 != null; var6 = var6.nextEntry)
            {
                if (var6.key == par1)
                {
                    var6.value = par3Obj;
                    return;
                }
            }

            ++this.modCount;
            this.createKey(var4, par1, par3Obj, var5);
        }

        private void resizeTable(int par1)
        {
            LongHashMapEntry[] var2 = this.hashArray;
            int var3 = var2.Length;

            if (var3 == 1073741824)
            {
                this.capacity = Int32.MaxValue;
            }
            else
            {
                LongHashMapEntry[] var4 = new LongHashMapEntry[par1];
                this.copyHashTableTo(var4);
                this.hashArray = var4;
                this.capacity = (int)((float)par1 * this.percentUseable);
            }
        }

        private void copyHashTableTo(LongHashMapEntry[] par1ArrayOfLongHashMapEntry)
        {
            LongHashMapEntry[] var2 = this.hashArray;
            int var3 = par1ArrayOfLongHashMapEntry.Length;

            for (int var4 = 0; var4 < var2.Length; ++var4)
            {
                LongHashMapEntry var5 = var2[var4];

                if (var5 != null)
                {
                    var2[var4] = null;
                    LongHashMapEntry var6;

                    do
                    {
                        var6 = var5.nextEntry;
                        int var7 = getHashIndex(var5.hash, var3);
                        var5.nextEntry = par1ArrayOfLongHashMapEntry[var7];
                        par1ArrayOfLongHashMapEntry[var7] = var5;
                        var5 = var6;
                    }
                    while (var6 != null);
                }
            }
        }

        public Object remove(long par1)
        {
            LongHashMapEntry var3 = this.removeKey(par1);
            return var3 == null ? null : var3.value;
        }

        LongHashMapEntry removeKey(long par1)
        {
            int var3 = getHashedKey(par1);
            int var4 = getHashIndex(var3, this.hashArray.Length);
            LongHashMapEntry var5 = this.hashArray[var4];
            LongHashMapEntry var6;
            LongHashMapEntry var7;

            for (var6 = var5; var6 != null; var6 = var7)
            {
                var7 = var6.nextEntry;

                if (var6.key == par1)
                {
                    ++this.modCount;
                    --this.numHashElements;

                    if (var5 == var6)
                    {
                        this.hashArray[var4] = var7;
                    }
                    else
                    {
                        var5.nextEntry = var7;
                    }

                    return var6;
                }

                var5 = var6;
            }

            return var6;
        }

        private void createKey(int par1, long par2, Object par4Obj, int par5)
        {
            LongHashMapEntry var6 = this.hashArray[par5];
            this.hashArray[par5] = new LongHashMapEntry(par1, par2, par4Obj, var6);

            if (this.numHashElements++ >= this.capacity)
            {
                this.resizeTable(2 * this.hashArray.Length);
            }
        }

        public static int getHashCode(long par0)
        {
            return getHashedKey(par0);
        }
    }
}
