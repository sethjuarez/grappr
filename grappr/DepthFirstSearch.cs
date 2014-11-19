using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class DepthFirstSearch : ISearchStrategy
    {
        readonly Stack<Node> _list;
        public DepthFirstSearch()
        {
            _list = new Stack<Node>();
        }

        public void Add(Node node)
        {
            _list.Push(node);
        }

        public int Count()
        {
            return _list.Count;
        }

        public Node Remove()
        {
            return _list.Pop();
        }
    }
}
