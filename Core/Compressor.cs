using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public class Compressor
    {
        private readonly Encoding encoding = Encoding.ASCII;

        public Compressor()
        {
        }

        public Compressor(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public Stream Compress(Stream input, Dictionary<byte, long> map)
        {
            var tree = new Tree();
            map = AnalyzeStreamForMap(input, map);

            using (var reader = new BinaryReader(input, encoding))
            {
                // TODO: Using byte map compress input stream and return it
            }

            // Current return statement help to compile app in dev environment
            return input;
        }

        private Dictionary<byte, long> AnalyzeStreamForMap(Stream input, Dictionary<byte, long> map)
        {
            using (var reader = new BinaryReader(input, encoding))
            {
                var length = reader.BaseStream.Length;

                while (reader.BaseStream.Position != length) map[reader.ReadByte()]++;
            }

            return map;
        }

        private Dictionary<byte, long> AnalyzeStreamForMap(Stream input)
        {
            return AnalyzeStreamForMap(input, InitEmptyMap());
        }

        private static Dictionary<byte, long> InitEmptyMap()
        {
            return new Dictionary<byte, long>(byte.MaxValue + 1);
        }
    }
}