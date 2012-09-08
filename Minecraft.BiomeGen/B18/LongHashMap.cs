using System;

namespace Minecraft.B18
{
    public class LongHashMap
    {
        private LongHashMapEntry[] hashArray;
        private int numHashElements;
        private int capacity;
        private float percent = 0.75F;
        private volatile int field_35581_e;


        public LongHashMap()
        {
            capacity = 12;
            hashArray = new LongHashMapEntry[16];
        }

        private static int getHashedKey(long l)
        {
            return hash((int)(l ^ l >> 32));
        }

        private static int hash(int i)
        {
            i ^= i >> 20 ^ i >> 12;
            return i ^ i >> 7 ^ i >> 4;
        }

        private static int getHashIndex(int i, int j)
        {
            return i & j - 1;
        }

        public int getNumHashElements()
        {
            return numHashElements;
        }

        public Object getValueByKey(long l)
        {
            int i = getHashedKey(l);
            for(LongHashMapEntry longhashmapentry = hashArray[getHashIndex(i, hashArray.Length)]; longhashmapentry != null; longhashmapentry = longhashmapentry.field_35833_c)
            {
                if(longhashmapentry.field_35834_a == l)
                {
                    return longhashmapentry.field_35832_b;
                }
            }

            return null;
        }

        public bool func_35575_b(long l)
        {
            return func_35569_c(l) != null;
        }

        LongHashMapEntry func_35569_c(long l)
        {
            int i = getHashedKey(l);
            for(LongHashMapEntry longhashmapentry = hashArray[getHashIndex(i, hashArray.Length)]; longhashmapentry != null; longhashmapentry = longhashmapentry.field_35833_c)
            {
                if(longhashmapentry.field_35834_a == l)
                {
                    return longhashmapentry;
                }
            }

            return null;
        }

        public void add(long l, Object obj)
        {
            int i = getHashedKey(l);
            int j = getHashIndex(i, hashArray.Length);
            for(LongHashMapEntry longhashmapentry = hashArray[j]; longhashmapentry != null; longhashmapentry = longhashmapentry.field_35833_c)
            {
                if(longhashmapentry.field_35834_a == l)
                {
                    longhashmapentry.field_35832_b = obj;
                }
            }

            field_35581_e++;
            createKey(i, l, obj, j);
        }

        private void resizeTable(int i)
        {
            LongHashMapEntry[] alonghashmapentry = hashArray;
            int j = alonghashmapentry.Length;
            if(j == 0x40000000)
            {
                capacity = 0x7fffffff;
                return;
            } else
            {
                LongHashMapEntry[] alonghashmapentry1 = new LongHashMapEntry[i];
                copyHashTableTo(alonghashmapentry1);
                hashArray = alonghashmapentry1;
                capacity = (int)((float)i * percent);
                return;
            }
        }

        private void copyHashTableTo(LongHashMapEntry[] alonghashmapentry)
        {
            LongHashMapEntry[] alonghashmapentry1 = hashArray;
            int i = alonghashmapentry.Length;
            for(int j = 0; j < alonghashmapentry1.Length; j++)
            {
                LongHashMapEntry longhashmapentry = alonghashmapentry1[j];
                if(longhashmapentry == null)
                {
                    continue;
                }
                alonghashmapentry1[j] = null;
                do
                {
                    LongHashMapEntry longhashmapentry1 = longhashmapentry.field_35833_c;
                    int k = getHashIndex(longhashmapentry.field_35831_d, i);
                    longhashmapentry.field_35833_c = alonghashmapentry[k];
                    alonghashmapentry[k] = longhashmapentry;
                    longhashmapentry = longhashmapentry1;
                } while(longhashmapentry != null);
            }

        }

        public Object remove(long l)
        {
            LongHashMapEntry longhashmapentry = removeKey(l);
            return longhashmapentry != null ? longhashmapentry.field_35832_b : null;
        }

        LongHashMapEntry removeKey(long l)
        {
            int i = getHashedKey(l);
            int j = getHashIndex(i, hashArray.Length);
            LongHashMapEntry longhashmapentry = hashArray[j];
            LongHashMapEntry longhashmapentry1;
            LongHashMapEntry longhashmapentry2;
            for(longhashmapentry1 = longhashmapentry; longhashmapentry1 != null; longhashmapentry1 = longhashmapentry2)
            {
                longhashmapentry2 = longhashmapentry1.field_35833_c;
                if(longhashmapentry1.field_35834_a == l)
                {
                    field_35581_e++;
                    numHashElements--;
                    if(longhashmapentry == longhashmapentry1)
                    {
                        hashArray[j] = longhashmapentry2;
                    } else
                    {
                        longhashmapentry.field_35833_c = longhashmapentry2;
                    }
                    return longhashmapentry1;
                }
                longhashmapentry = longhashmapentry1;
            }

            return longhashmapentry1;
        }

        private void createKey(int i, long l, Object obj, int j)
        {
            LongHashMapEntry longhashmapentry = hashArray[j];
            hashArray[j] = new LongHashMapEntry(i, l, obj, longhashmapentry);
            if(numHashElements++ >= capacity)
            {
                resizeTable(2 * hashArray.Length);
            }
        }

        public static int getHashCode(long l)
        {
            return getHashedKey(l);
        }

    }
}
