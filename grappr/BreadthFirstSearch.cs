using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class BreadthFirstSearch : ISearchStrategy
    {
        readonly Queue<Node> _list;
        public BreadthFirstSearch()
        {
            _list = new Queue<Node>();
        }

        public void Add(Node node)
        {
            _list.Enqueue(node);
        }

        public int Count()
        {
            return _list.Count;
        }

        public Node Remove()
        {
            return _list.Dequeue();
        }
    }
}