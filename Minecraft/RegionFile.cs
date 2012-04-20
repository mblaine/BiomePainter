using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zlib;

namespace Minecraft
{
    public class RegionFile
    {
        public Chunk[,] Chunks;
        public Coord Coords;
        public String Path;
        public bool Dirty = false;

        public RegionFile()
        {
            Chunks = new Chunk[32, 32];
        }

        public RegionFile(String path)
        {
            Path = path;
            Read(Path);
        }

        public RegionFile(String path, int startX, int endX, int startZ, int endZ)
        {
            Path = path;
            Read(Path, startX, endX, startZ, endZ);
        }


        //DO NOT write region if reading less than the entire thing
        public void Read(String path)
        {
            Read(path, 0, 31, 0, 31);
        }

        //http://www.minecraftwiki.net/wiki/Region_file_format
        public void Read(String path, int startX, int endX, int startZ, int endZ)
        {
            Chunks = new Chunk[32, 32];
            Match m = Regex.Match(path, @"r\.(-?\d+)\.(-?\d+)\.mca");
            Coords.X = int.Parse(m.Groups[1].Value);
            Coords.Z = int.Parse(m.Groups[2].Value);

            if (!File.Exists(path))
                return;

            byte[] header = new byte[8192];

            using (BinaryReader file = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                file.Read(header, 0, 8192);

                for (int chunkZ = startZ; chunkZ <= endZ; chunkZ++)
                {
                    for (int chunkX = startX; chunkX <= endX; chunkX++)
                    {
                        Chunk c = new Chunk();
                        c.Coords.X = Coords.X;
                        c.Coords.Z = Coords.Z;
                        c.Coords.RegiontoChunk();
                        c.Coords.Add(chunkX, chunkZ);


                        int i = 4 * (chunkX + chunkZ * 32);

                        byte[] temp = new byte[4];
                        temp[0] = 0;
                        Array.Copy(header, i, temp, 1, 3);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        long offset = ((long)BitConverter.ToInt32(temp, 0)) * 4096;
                        int length = header[i + 3] * 4096;

                        temp = new byte[4];
                        Array.Copy(header, i + 4096, temp, 0, 4);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        c.Timestamp = BitConverter.ToInt32(temp, 0);

                        if (offset == 0 && length == 0)
                        {
                            Chunks[chunkX, chunkZ] = c;
                            continue;
                        }

                        file.BaseStream.Seek(offset, SeekOrigin.Begin);

                        temp = new byte[4];
                        file.Read(temp, 0, 4);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        int exactLength = BitConverter.ToInt32(temp, 0);

                        c.CompressionType = file.ReadByte();
                        if (c.CompressionType == 1) //GZip
                        {
                            c.RawData = new byte[exactLength - 1];
                            file.Read(c.RawData, 0, exactLength - 1);

                            GZipStream decompress = new GZipStream(new MemoryStream(c.RawData), CompressionMode.Decompress);
                            MemoryStream mem = new MemoryStream();
                            decompress.CopyTo(mem);
                            mem.Seek(0, SeekOrigin.Begin);
                            c.Root = new TAG_Compound(mem);
                        }
                        else if (c.CompressionType == 2) //Zlib
                        {
                            c.RawData = new byte[exactLength - 1];
                            file.Read(c.RawData, 0, exactLength - 1);

                            ZlibStream decompress = new ZlibStream(new MemoryStream(c.RawData), CompressionMode.Decompress);
                            MemoryStream mem = new MemoryStream();
                            decompress.CopyTo(mem);
                            mem.Seek(0, SeekOrigin.Begin);
                            c.Root = new TAG_Compound(mem);
                        }
                        else
                        {
                            throw new Exception("Unrecognized compression type");
                        }

                        Chunks[chunkX, chunkZ] = c;
                    }
                }

                file.Close();
            }
        }

        //DO NOT write region if reading less than the entire thing
        public void Write(bool force = false)
        {
            Write(Path, force);
        }

        public void Write(String path, bool force = false)
        {
            if (!force && !Dirty)
                return;
            byte[] header = new byte[8192];
            Array.Clear(header, 0, 8192);

            Int32 sectorOffset = 2;
            using (BinaryWriter file = new BinaryWriter(File.Exists(path) ? File.Open(path, FileMode.Truncate) : File.Open(path, FileMode.Create)))
            {
                file.Write(header, 0, 8192);

                for (int chunkX = 0; chunkX < 32; chunkX++)
                {
                    for (int chunkZ = 0; chunkZ < 32; chunkZ++)
                    {
                        Chunk c = Chunks[chunkX, chunkZ];
                        if (c == null)
                            continue;
                    
                        int i = 4 * (chunkX + chunkZ * 32);

                        byte[] temp = BitConverter.GetBytes(c.Timestamp);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        Array.Copy(temp, 0, header, i + 4096, 4);

                        if (c.Root == null)
                        {
                            Array.Clear(temp, 0, 4);
                            Array.Copy(temp, 0, header, i, 4);
                            continue;
                        }

                        temp = BitConverter.GetBytes(sectorOffset);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);
                        Array.Copy(temp, 1, header, i, 3);

                        if (c.RawData == null || force || c.Dirty)
                        {
                            //this is the performance bottleneck when doing 1024 chunks in a row;
                            //trying to only do when necessary
                            MemoryStream mem = new MemoryStream();
                            ZlibStream zlib = new ZlibStream(mem, CompressionMode.Compress);
                            c.Root.Write(zlib);
                            zlib.Close();
                            c.RawData = mem.ToArray();
                            c.CompressionType = 2;
                        }

                        temp = BitConverter.GetBytes(c.RawData.Length + 1);
                        if (BitConverter.IsLittleEndian)
                            Array.Reverse(temp);

                        file.Write(temp, 0, 4);
                        file.Write(c.CompressionType);
                        file.Write(c.RawData, 0, c.RawData.Length);

                        byte[] padding = new byte[(4096 - ((c.RawData.Length + 5) % 4096))];
                        Array.Clear(padding, 0, padding.Length);
                        file.Write(padding);

                        header[i + 3] = (byte)((c.RawData.Length + 5) / 4096 + 1);
                        sectorOffset += (c.RawData.Length + 5) / 4096 + 1;
                        c.Dirty = false;
                    }
                }

                file.Seek(0, SeekOrigin.Begin);
                file.Write(header, 0, 8192);
                file.Flush();
                file.Close();
                Dirty = false;
            }

        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Region [{0}, {1}]{2}{{{2}", Coords.X, Coords.Z, Environment.NewLine);
            foreach (Chunk c in Chunks)
                sb.Append(c.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static String ToString(String path)
        {
            Match m = Regex.Match(path, @"r\.(-?\d+)\.(-?\d+)\.mca");
            Coord c = new Coord(int.Parse(m.Groups[1].Value),int.Parse(m.Groups[2].Value));
            Coord c2 = new Coord(int.Parse(m.Groups[1].Value) + 1, int.Parse(m.Groups[2].Value) + 1);
            c.RegiontoAbsolute();
            c2.RegiontoAbsolute();
            return String.Format("Region {0}, {1} :: ({2}, {3}) to ({4}, {5})", m.Groups[1].Value, m.Groups[2].Value, c.X, c.Z, c2.X - 1, c2.Z - 1);
        }
    }
}
