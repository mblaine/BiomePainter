using System;
using System.Collections.Generic;
using Minecraft;

namespace BiomePainter.History
{
    public class ChunkState
    {
        public Coord Coords;
        public byte[] Biomes = null;

        public ChunkState(int x, int z, byte[] biomes)
        {
            Coords.X = x;
            Coords.Z = z;
            Biomes = biomes;
        }
    }

    public class BiomeAction : IAction
    {
        public List<ChunkState> Chunks = new List<ChunkState>();

        public void Dispose()
        {
        }
    }
}
