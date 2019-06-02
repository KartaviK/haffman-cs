using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmunCore
{
    public interface INode
    {
        Int64 SummuryWeight();
        Boolean IsLeaf();
    }
}
