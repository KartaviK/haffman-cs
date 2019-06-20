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
            var map = AnalyzeStreamForMap(input);

            map.OrderBy(key => key.Value);

            return Compress(
                input,
                output,
                Tree.Fill(map).ToPriceMap()
            );
        }

        public Archive Compress(Stream input, Stream output, Dictionary<char, BitArray> bytePrice)
        {
            using (var reader = new BinaryReader(input, encoding, true))
            using (var writer = new BinaryWriter(output, encoding, true))
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

            using (var reader = new BinaryReader(archive.Data, encoding, true))
            using (var writer = new BinaryWriter(output, encoding, true))
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

        private Dictionary<char, long> AnalyzeStreamForMap(Stream input, Dictionary<char, long> map)
        {
            using (var reader = new BinaryReader(input, encoding, true))
            {
                var length = reader.BaseStream.Length;
                char tempTargetByte;

                while (reader.BaseStream.Position != length)
                {
                    tempTargetByte = reader.ReadChar();

                    if (map.ContainsKey(tempTargetByte))
                        map[tempTargetByte]++;
                    else
                       map.Add(tempTargetByte, 1);
                }
            }

            input.Position = 0;

            return map;
        }

        private Dictionary<char, long> AnalyzeStreamForMap(Stream input)
        {
            return AnalyzeStreamForMap(input, InitEmptyMap());
        }

        private static Dictionary<char, long> InitEmptyMap()
        {
            return new Dictionary<char, long>(byte.MaxValue + 1);
        }
    }
}
