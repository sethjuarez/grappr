using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public interface ISearchStrategy
    {
        void Add(Node node);
        int Count();
        Node Peek();
        Node Remove();

    }
}
