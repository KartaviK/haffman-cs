using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace Core
{
    public static class Extension
    {
        public static Archive Deserialize(this Stream encoded)
        {
            var tree = new Tree();
            var map = new Dictionary<char, long>();

            using (var reader = new BinaryReader(encoded))
            {
                var sequencesCount = reader.ReadByte();

                for (byte i = 0; i < sequencesCount; i++)
                {
                    map.Add(reader.ReadChar(), reader.ReadByte());
                }
            }

            tree.Fill(map);

            return new Archive(encoded, tree.ToPriceMap());
        }
    }
}
