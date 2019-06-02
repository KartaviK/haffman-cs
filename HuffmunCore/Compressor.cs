using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HuffmunCore
{
    public class Compressor
    {
        protected Encoding encoding = Encoding.ASCII;

        public Compressor() { }

        public Compressor(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public Stream Compress(Stream input, Dictionary<Byte, Int64> map)
        {
            Tree tree = new Tree();
            map = AnalizeStreamForMap(input, map);

            using (var reader = new BinaryReader(input, encoding))
            {


            }
            
        }

        public Dictionary<Byte, Int64> AnalizeStreamForMap(Stream input, Dictionary<Byte, Int64> map)
        {
            using (var reader = new BinaryReader(input, encoding))
            {
                Int64 length = (Int64)reader.BaseStream.Length;

                while (reader.BaseStream.Position != length)
                {
                    map[reader.ReadByte()]++;
                }
            }

            return map;
        }

        public Dictionary<Byte, Int64> AnalizeStreamForMap(Stream input)
        {
            return this.AnalizeStreamForMap(input, this.InitEmptyMap());
        }

        protected Dictionary<Byte, Int64> InitEmptyMap()
        {
            var map = new Dictionary<Byte, Int64>(256);

            for (Byte i = 0; i < Byte.MaxValue; i++)
            {
                map[i] = 0;
            }

            return map;
        }
    }
}
