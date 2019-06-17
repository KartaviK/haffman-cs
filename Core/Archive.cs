using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Core
{
    public class Archive
    {
        public Stream Data { get; }
        public Dictionary<byte, BitArray> BytePrice { get; }

        public Archive(Stream data, Dictionary<byte, BitArray> bytePrice)
        {
            Data = data;
            BytePrice = bytePrice;
        }

        public Stream Serialize()
        {
            var tempStream = new MemoryStream();

            using (var writer = new BinaryWriter(tempStream))
            {
                writer.Write((byte)BytePrice.Count);

                foreach (var priceOfByte in BytePrice)
                {
                    writer.Write(priceOfByte.Key);
                    writer.Write((byte)priceOfByte.Value.Count);
                    
                    foreach (bool bit in priceOfByte.Value)
                    {
                        writer.Write(bit);
                    }
                }
            }

            return tempStream;
        }
    }
}
