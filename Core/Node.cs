using System;

namespace Core
{
    /// <summary>
    ///     Represents binary tree's indivisible element
    /// </summary>
    [Serializable]
    public class Node
    {
        [NonSerialized] private long weight;

        public byte Value { get; set; }

        public long Weight
        {
            get => IsLeaf() ? weight : SummaryWeight();
            set => weight = value;
        }

        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
        }

        public Node(byte value, long weight)
        {
            Value = value;
            this.weight = weight;
        }

        public long SummaryWeight()
        {
            var rightWeight = Right?.SummaryWeight() ?? 0;
            var leftWeight = Left?.SummaryWeight() ?? 0;

            return leftWeight + rightWeight + weight;
        }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }
    }
}