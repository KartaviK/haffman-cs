using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Core
{
    [Serializable]
    public class Archive
    {
        private Stream data;
        private Dictionary<byte, BitArray> bytePrice;

        public Stream Data => data;
        public Dictionary<byte, BitArray> BytePrice => bytePrice;

        public Archive(Stream data, Dictionary<byte, BitArray> bytePrice)
        {
            this.data = data;
            this.bytePrice = bytePrice;
        }
    }
}
