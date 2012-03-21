using System;

namespace Minecraft
{
    public struct Coord
    {
        public int x;
        public int z;

        public Coord(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public Coord(Coord c)
        {
            this.x = c.x;
            this.z = c.z;
        }

        public void Add(int dx, int dz)
        {
            x += dx;
            z += dz;
        }

        public void ChunktoAbsolute()
        {
            x *= 16;
            z *= 16;
        }

        public void AbsolutetoChunk()
        {
            x = (int)Math.Floor(((double)x) / 16.0);
            z = (int)Math.Floor(((double)z) / 16.0);
        }

        public void RegiontoAbsolute()
        {
            x = x * 16 * 32;
            z = z * 16 * 32;
        }

        public void AbsolutetoRegion()
        {
            x = (int)Math.Floor(((double)x) / 32.0 / 16.0);
            z = (int)Math.Floor(((double)z) / 32.0 / 16.0);
        }

        public void RegiontoChunk()
        {
            x = x * 32;
            z = z * 32;
        }

        public void ChunktoRegion()
        {
            x = (int)Math.Floor(((double)x) / 32.0);
            z = (int)Math.Floor(((double)z) / 32.0);
        }
    }
}
