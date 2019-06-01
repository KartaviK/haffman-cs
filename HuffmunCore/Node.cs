using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmunCore
{
    [Serializable]
    public class Node
    {
        [NonSerialized]
        private Int64 count;

        public Byte Value { get; set; }
        public Int64 Count { get => count; set => count = value; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node() { }

        public Node(Byte value, Int64 count)
        {
            this.Value = value;
            this.count = count;
        }
    }
}
