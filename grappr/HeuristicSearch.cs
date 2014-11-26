using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public abstract class HeuristicSearch : ISearchStrategy
    {
        private readonly PriorityQueue<double, Node> _queue;
        public Func<IState, double> Heuristic { get; set; }

        public HeuristicSearch()
        {
            _queue = new PriorityQueue<double, Node>();
        }

        public abstract void Add(Node node);

        public void Add(Node node, double cost)
        {
            _queue.Enqueue(cost, node);
        }

        public int Count()
        {
            return _queue.Count;
        }

        public Node Remove()
        {
            return _queue.Dequeue() as Node;
        }
    }
}
