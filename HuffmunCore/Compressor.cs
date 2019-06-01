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

        public bool Compress(Stream input, string pathToSave)
        {
            using (var reader = new BinaryReader(input, encoding))
            {
                Tree tree = new Tree();
                Dictionary<Byte, Int64> byteMap = InitByteMap();
                Int64 length = (Int64)reader.BaseStream.Length;

                while (reader.BaseStream.Position != length)
                {
                    Byte currentByte = reader.ReadByte();
                    byteMap[currentByte]++;
                }

                foreach(KeyValuePair<byte, Int64> charCount in charsCounts.OrderBy(key => key.Value))
                {
                    tree.Insert(charCount.Key);
                }
            }

            return true;
        }

        protected Dictionary<Byte, Int64> InitByteMap()
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
