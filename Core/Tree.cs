using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Tree
    {
        private Node root;

        public Node Root => root;

        public Tree Fill(Dictionary<byte, long> map)
        {
            if (map.Count < 2)
            {
                throw new ArgumentException("Binary map must contain more than 2 elements");
            }

            var nodes = new List<Node>();

            map.OrderBy(key => key.Value)
                .ToList()
                .ForEach(pair => nodes.Add(new Node(pair.Key, pair.Value)));

            while (nodes.Count >= 2)
            {
                var node = new Node {Left = nodes[0], Right = nodes[1]};
                nodes.RemoveRange(0, 2);

                var index = nodes.FindIndex(element => element.SummaryWeight() >= node.SummaryWeight());
                nodes.Insert(index < 0 ? 0 : index, node);
            }

            root = nodes.First();

            return this;
        }

        public Dictionary<byte, BitArray> PriceMap()
        {
            var map = new Dictionary<byte, BitArray>();

            for (byte value = 0; value < byte.MaxValue; value++)
            {
                var price = CalculateBytePrice(value);

                if (price != null)
                {
                    map.Add(value, new BitArray(price.ToArray()));
                }
            }

            return map;
        }

        private List<bool> CalculateBytePrice(byte value)
        {
            return CalculateBytePrice(Root, value, new List<bool>());
        }

        private static List<bool> CalculateBytePrice(Node target, byte value, List<bool> price)
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