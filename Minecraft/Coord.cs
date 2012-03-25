using System;

namespace Minecraft
{
    public struct Coord
    {
        public int X;
        public int Z;

        public Coord(int x, int z)
        {
            this.X = x;
            this.Z = z;
        }

        public Coord(Coord c)
        {
            this.X = c.X;
            this.Z = c.Z;
        }

        public void Add(int dx, int dz)
        {
            X += dx;
            Z += dz;
        }

        public void ChunktoAbsolute()
        {
            X *= 16;
            Z *= 16;
        }

        public void AbsolutetoChunk()
        {
            X = (int)Math.Floor(((double)X) / 16.0);
            Z = (int)Math.Floor(((double)Z) / 16.0);
        }

        public void RegiontoAbsolute()
        {
            X = X * 16 * 32;
            Z = Z * 16 * 32;
        }

        public void AbsolutetoRegion()
        {
            X = (int)Math.Floor(((double)X) / 32.0 / 16.0);
            Z = (int)Math.Floor(((double)Z) / 32.0 / 16.0);
        }

        public void RegiontoChunk()
        {
            X = X * 32;
            Z = Z * 32;
        }

        public void ChunktoRegion()
        {
            X = (int)Math.Floor(((double)X) / 32.0);
            Z = (int)Math.Floor(((double)Z) / 32.0);
        }
    }
}
