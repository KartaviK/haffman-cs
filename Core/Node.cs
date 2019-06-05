using System;

namespace Core
{
    /// <summary>
    /// Represents binary tree's indivisible element
    /// </summary>
    [Serializable]
    public class Node
    {
        [NonSerialized] private long weight;

        public byte Value { get; set; }

        public long Weight
        {
            get => weight;
            set => weight = value;
        }

        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
        }

        public Node(byte value, long weight)
        {
            this.Value = value;
            this.weight = weight;
        }

        public long SummaryWeight()
        {
            var rightWeight = this.Right?.SummaryWeight() ?? 0;
            var leftWeight = this.Left?.SummaryWeight() ?? 0;

            return leftWeight + rightWeight + this.Weight;
        }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }
    }
}