using System;
using System.Collections;

namespace HuffmunCore
{
    /// <summary>
    /// Represents binary tree's indivisible element
    /// </summary>
    [Serializable]
    public class Node : INode
    {
        [NonSerialized]
        private Int64 weight;

        public Byte Value { get; set; }
        public Int64 Weight { get => weight; set => weight = value; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node() { }

        public Node(Byte value, Int64 weight)
        {
            this.Value = value;
            this.weight = weight;
        }

        public Int64 SummuryWeight()
        {
            var rightWeight = this.Right?.SummuryWeight() ?? 0;
            var leftWeight = this.Left?.SummuryWeight() ?? 0;

            return leftWeight + rightWeight + this.Weight;
        }

        public Boolean IsLeaf()
        {
            return Left == null && Right == null;
        }
    }
}
