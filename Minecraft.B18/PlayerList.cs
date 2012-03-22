using System;

namespace Minecraft.B18
{
    public class PlayerList
    {
        private PlayerListEntry[] playerListEntries;
        private int numHashElements;
        private int capacity;
        private float percent = 0.75F;
        private volatile int field_35581_e;


        public PlayerList()
        {
            capacity = 12;
            playerListEntries = new PlayerListEntry[16];
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
            for(PlayerListEntry playerlistentry = playerListEntries[getHashIndex(i, playerListEntries.Length)]; playerlistentry != null; playerlistentry = playerlistentry.field_35833_c)
            {
                if(playerlistentry.field_35834_a == l)
                {
                    return playerlistentry.field_35832_b;
                }
            }

            return null;
        }

        public bool func_35575_b(long l)
        {
            return func_35569_c(l) != null;
        }

        PlayerListEntry func_35569_c(long l)
        {
            int i = getHashedKey(l);
            for(PlayerListEntry playerlistentry = playerListEntries[getHashIndex(i, playerListEntries.Length)]; playerlistentry != null; playerlistentry = playerlistentry.field_35833_c)
            {
                if(playerlistentry.field_35834_a == l)
                {
                    return playerlistentry;
                }
            }

            return null;
        }

        public void add(long l, Object obj)
        {
            int i = getHashedKey(l);
            int j = getHashIndex(i, playerListEntries.Length);
            for(PlayerListEntry playerlistentry = playerListEntries[j]; playerlistentry != null; playerlistentry = playerlistentry.field_35833_c)
            {
                if(playerlistentry.field_35834_a == l)
                {
                    playerlistentry.field_35832_b = obj;
                }
            }

            field_35581_e++;
            createKey(i, l, obj, j);
        }

        private void resizeTable(int i)
        {
            PlayerListEntry[] aplayerlistentry = playerListEntries;
            int j = aplayerlistentry.Length;
            if(j == 0x40000000)
            {
                capacity = 0x7fffffff;
                return;
            } else
            {
                PlayerListEntry[] aplayerlistentry1 = new PlayerListEntry[i];
                copyHashTableTo(aplayerlistentry1);
                playerListEntries = aplayerlistentry1;
                capacity = (int)((float)i * percent);
                return;
            }
        }

        private void copyHashTableTo(PlayerListEntry[] aplayerlistentry)
        {
            PlayerListEntry[] aplayerlistentry1 = playerListEntries;
            int i = aplayerlistentry.Length;
            for(int j = 0; j < aplayerlistentry1.Length; j++)
            {
                PlayerListEntry playerlistentry = aplayerlistentry1[j];
                if(playerlistentry == null)
                {
                    continue;
                }
                aplayerlistentry1[j] = null;
                do
                {
                    PlayerListEntry playerlistentry1 = playerlistentry.field_35833_c;
                    int k = getHashIndex(playerlistentry.field_35831_d, i);
                    playerlistentry.field_35833_c = aplayerlistentry[k];
                    aplayerlistentry[k] = playerlistentry;
                    playerlistentry = playerlistentry1;
                } while(playerlistentry != null);
            }

        }

        public Object remove(long l)
        {
            PlayerListEntry playerlistentry = removeKey(l);
            return playerlistentry != null ? playerlistentry.field_35832_b : null;
        }

        PlayerListEntry removeKey(long l)
        {
            int i = getHashedKey(l);
            int j = getHashIndex(i, playerListEntries.Length);
            PlayerListEntry playerlistentry = playerListEntries[j];
            PlayerListEntry playerlistentry1;
            PlayerListEntry playerlistentry2;
            for(playerlistentry1 = playerlistentry; playerlistentry1 != null; playerlistentry1 = playerlistentry2)
            {
                playerlistentry2 = playerlistentry1.field_35833_c;
                if(playerlistentry1.field_35834_a == l)
                {
                    field_35581_e++;
                    numHashElements--;
                    if(playerlistentry == playerlistentry1)
                    {
                        playerListEntries[j] = playerlistentry2;
                    } else
                    {
                        playerlistentry.field_35833_c = playerlistentry2;
                    }
                    return playerlistentry1;
                }
                playerlistentry = playerlistentry1;
            }

            return playerlistentry1;
        }

        private void createKey(int i, long l, Object obj, int j)
        {
            PlayerListEntry playerlistentry = playerListEntries[j];
            playerListEntries[j] = new PlayerListEntry(i, l, obj, playerlistentry);
            if(numHashElements++ >= capacity)
            {
                resizeTable(2 * playerListEntries.Length);
            }
        }

        public static int getHashCode(long l)
        {
            return getHashedKey(l);
        }

    }
}
