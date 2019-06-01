using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmunCore
{
    [Serializable]
    internal class Tree
    {
        private Node root;
        
        public Tree()
        {
            this.root = new Node();
        }

        delegate Int64 pairProcess(KeyValuePair<Byte, Int64> pair);

        public void Fill(Dictionary<Byte, Int64> map)
        {
            List<Node> nodes = new List<Node>();

            foreach(var pair in map)
            {
                nodes.Add(new Node(pair.Key, pair.Value));
            }


        }

        public void Add(Byte value)
        {

        }

        protected Int64 ByValue(KeyValuePair<Byte, Int64> pair)
        {
            return pair.Value;
        }
    }
}
