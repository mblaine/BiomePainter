using System;
using System.Drawing;
using Minecraft;

namespace BiomePainter.Clipboard
{
    [Serializable]
    public class BiomeCopy
    {
        public byte[,] Biomes;
        public int Top = 511;
        public int Left = 511;
        public int Right = 0;
        public int Bottom = 0;
        public int Width = 512;
        public int Height = 512;
        public bool Empty = true;

        public BiomeCopy()
        {
            Biomes = new byte[Width, Height];
            for (int x = 0; x < Width; x++)
                for (int z = 0; z < Height; z++)
                    Biomes[x, z] = (byte)Biome.Unspecified;
        }

        public void Crop()
        {
            if (Empty)
                return;
            Width = Right - Left + 1;
            Height = Bottom - Top + 1;
            byte[,] b = new byte[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int z = 0; z < Height; z++)
                {
                    b[x, z] = Biomes[Left + x, Top + z];
                }
            }
            Biomes = b;
        }

        public Bitmap ToBitmap()
        {
            Bitmap b = new Bitmap(Width, Height);
            for (int x = 0; x < Width; x++)
            {
                for (int z = 0; z < Height; z++)
                {
                    Color c = Color.Black;
                    byte biome = Biomes[x, z];
                    if (biome == 255)
                        c = Color.Transparent;
                    else if (biome >= 0 && biome < 255 && BiomeType.Biomes[biome] != null)
                        c = BiomeType.Biomes[biome].Color;
                    b.SetPixel(x, z, c);
                }
            }
            return b;
        }
    }
}
