using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class DepthLimitedSearch : ISearchStrategy
    {
        readonly Stack<Node> _list;
        private readonly int _limit;
        public DepthLimitedSearch(int limit)
        {
            _limit = limit;
            _list = new Stack<Node>();
        }

        public void Add(Node node)
        {
            if (node.Depth <= _limit)
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
