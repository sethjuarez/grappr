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

        public Node Peek()
        {
            return _list.Peek();
        }

        public Node Remove()
        {
            return _list.Dequeue();
        }
    }

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

        public Node Peek()
        {
            return _list.Peek();
        }

        public Node Remove()
        {
            return _list.Pop();
        }
    }

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

        public Node Peek()
        {
            return _list.Peek();
        }

        public Node Remove()
        {
            return _list.Pop();
        }
    }
}
