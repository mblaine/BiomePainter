using System;
using System.Text;

namespace Minecraft
{
    public class Chunk
    {
        public TAG_Compound Root;
        public Coord Coords = new Coord();
        public Int32 Timestamp;
        public bool Dirty = false;
        public byte[] RawData = null;

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            DateTime time = new DateTime(1970, 1, 1).AddSeconds(Timestamp);

            sb.AppendFormat("Chunk [{0}, {1}] {2:M/d/yyyy h:mm:ss tt}\r\n{{\r\n", Coords.x, Coords.z, time);
            if (Root != null)
                sb.Append(Root.ToString());
            sb.Append("}\r\n");
            return sb.ToString();
        }
    }
}
