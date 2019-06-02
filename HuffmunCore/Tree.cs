using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmunCore
{
    [Serializable]
    internal class Tree
    {
        private Node root;

        public Tree() { }

        public void Fill(Dictionary<Byte, Int64> map)
        {
            if (map.Count < 2)
            {
                throw new ArgumentException("Binary map must contain more than 2 elements");
            }

            List<Node> nodes = new List<Node>();

            foreach (var pair in map.OrderBy(key => key.Value))
            {
                nodes.Add(new Node(pair.Key, pair.Value));
            }

            while (nodes.Count >= 2)
            {
                var node = new Node { Left = nodes[0], Right = nodes[1] };
                nodes.RemoveRange(0, 2);

                nodes.Insert(
                    nodes.FindIndex(element => element.SummuryWeight() >= node.SummuryWeight()),
                    node
                );
            }

            this.root = nodes.First();
        }

        public Dictionary<Byte, BitArray> PriceMap()
        {
            var priceMap = new Dictionary<Byte, BitArray>();
            var node = root;

            do
            {
                List<bool> price= new List<bool>();

                if (node.Right != null)
                {
                    price.Add
                }

                if (node.IsLeaf())
                {
                    if (!priceMap.ContainsKey(node.Value))
                    {
                        priceMap[node.Value][]
                    }


                }
            }
        }
    }
}
