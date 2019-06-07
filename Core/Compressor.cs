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
        private Tree tree;

        public Tree Tree => tree;

        public Compressor()
        {
            this.tree = new Tree();
        }

        public Compressor(Encoding encoding, Tree tree = null)
        {
            this.encoding = encoding;
            this.tree = tree ?? new Tree();
        }

        /// <summary>
        ///     Encodes data using Huffman algorithm
        /// </summary>
        /// <param name="input">Stream of data needs to encode</param>
        /// <param name="output">Stream to write encoded data</param>
        /// <returns>Object with stream of encoded data with byte price dictionary. It is need to serialize</returns>
        public Archive Compress(Stream input, Stream output)
        {
            return Compress(
                input,
                output,
                tree.Fill(
                    AnalyzeStreamForMap(input)
                ).PriceMap()
            );
        }

        public Archive Compress(Stream input, Stream output, Dictionary<byte, BitArray> bytePrice)
        {
            using (var reader = new BinaryReader(input, encoding))
            {
                using (var writer = new BinaryWriter(output, encoding))
                {
                    var bits = bytePrice[reader.ReadByte()];

                    foreach (bool bit in bits)
                    {
                        writer.Write(bit);
                    }
                }
            }

            return new Archive(output, bytePrice);
        }

        public Stream Decompress(Archive arcive, Stream output)
        {
            var bytesPrices = arcive.BytePrice.OrderBy(pair => pair.Key);
            var bits = new List<bool[]>(arcive.BytePrice.Count);

            foreach (var bytePrice in bytesPrices)
            {
                var convertedPrice = new bool[bytePrice.Value.Count];

                foreach (bool bit in bytePrice.Value)
                {
                    convertedPrice.Append(bit);
                }

                bits.Add(convertedPrice);
            }

            using (var reader = new BinaryReader(arcive.Data, encoding))
            {
                using (var writer = new BinaryWriter(output, encoding))
                {
                    foreach (bool bit in bits[reader.ReadByte()])
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