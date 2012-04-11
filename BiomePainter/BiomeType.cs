using System;
using System.Drawing;

namespace BiomePainter
{
    public class BiomeType : IComparable
    {
        public byte ID;
        public String Name;
        public Color Color;

        public BiomeType(byte id, String name, int color)
        {
            ID = id;
            Name = name;
            Color = Color.FromArgb(255, Color.FromArgb(color));
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            if (obj is BiomeType)
                return Name.CompareTo(((BiomeType)obj).Name);
            else
                return 0;
        }
    }
}
