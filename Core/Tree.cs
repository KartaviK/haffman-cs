using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Tree
    {
        public Node Root { get; private set; }

        public int Count { get; private set; }

        public Tree Fill(Dictionary<char, long> map)
        {
            if (map.Count < 2)
            {
                throw new ArgumentException("Binary map must contain more than 2 elements");
            }

            var nodes = new List<Node>();

            Count = map.Count;
            map.ToList().ForEach(pair => nodes.Add(new Node(pair.Key, pair.Value)));

            while (nodes.Count >= 2)
            {
                var node = new Node {Left = nodes[0], Right = nodes[1]};
                nodes.RemoveRange(0, 2);

                var index = nodes.FindIndex(element => element.SummaryWeight() >= node.SummaryWeight());
                nodes.Insert(index < 0 ? 0 : index, node);
            }

            Root = nodes.First();

            return this;
        }

        public Dictionary<byte, BitArray> ToPriceMap()
        {
            var map = new Dictionary<char, BitArray>();

            for (int value = 0; value < this.Count; value++)
            {
                var price = CalculateBytePrice(value);

                if (price != null)
                {
                    map.Add(value, new BitArray(price.ToArray()));
                }
            }

            return map;
        }

        private List<bool> CalculateBytePrice(char value)
        {
            return CalculateBytePrice(Root, value, new List<bool>());
        }

        private static List<bool> CalculateBytePrice(Node target, char value, List<bool> price)
        {
            if (target.IsLeaf())
            {
                return target.Value.Equals(value) ? price : null;
            }

            List<bool> left = null;
            List<bool> right = null;

            if (target.Left != null)
            {
                var path = new List<bool>();
                
                path.AddRange(price);
                path.Add(false);

                left = CalculateBytePrice(target.Left, value, path);
            }

            if (target.Right != null)
            {
                var path = new List<bool>();
                
                path.AddRange(price);
                path.Add(true);

                right = CalculateBytePrice(target.Right, value, path);
            }

            return left ?? right;
        }
    }
}
