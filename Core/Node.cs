namespace Core
{
    public class Node
    {
        private readonly long weight;

        public char? Value { get; }

        public long Weight
        {
            get => IsLeaf() ? weight : SummaryWeight();
        }

        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
        }

        public Node(char value, long weight)
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