using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;
using System.Linq;

namespace Core
{
    public class Compressor
    {
        private readonly Encoding encoding = Encoding.ASCII;

        private Tree Tree { get; }

        public Compressor(Tree tree = null)
        {
            Tree = tree ?? new Tree();
        }

        public Archive Compress(Stream input, Stream output)
        {
            return Compress(
                input,
                output,
                Tree.Fill(AnalyzeStreamForMap(input)).ToPriceMap()
            );
        }

        public Archive Compress(Stream input, Stream output, Dictionary<byte, BitArray> bytePrice)
        {
            using (var reader = new BinaryReader(input, encoding))
            using (var writer = new BinaryWriter(output, encoding))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    foreach (bool bit in bytePrice[reader.ReadByte()])
                    {
                        writer.Write(bit);
                    }
                }
            }

            return new Archive(output, bytePrice);
        }

        public Stream Decompress(Archive archive, Stream output)
        {
            var bytesPrices = archive.BytePrice.OrderBy(pair => pair.Key);
            var bits = new List<bool[]>(archive.BytePrice.Count);

            foreach (var bytePrice in bytesPrices)
            {
                var convertedPrice = new bool[bytePrice.Value.Count];

                foreach (bool bit in bytePrice.Value)
                {
                    convertedPrice.Append(bit);
                }

                bits.Add(convertedPrice);
            }

            using (var reader = new BinaryReader(archive.Data, encoding))
            using (var writer = new BinaryWriter(output, encoding))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    foreach (var bit in bits[reader.ReadByte()])
                    {
                        writer.Write(bit);
                    }
                }
            }

            return output;
        }

        private Dictionary<byte, long> AnalyzeStreamForMap(Stream input, Dictionary<byte, long> map)
        {
            using (var reader = new BinaryReader(input, encoding))
            {
                var length = reader.BaseStream.Length;

                while (reader.BaseStream.Position != length)
                {
                    map[reader.ReadByte()]++;
                }
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
