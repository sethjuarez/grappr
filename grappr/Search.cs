using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class Search
    {
        private readonly ISearchStrategy _strategy;
        private readonly List<IState> _closed;

        public Search(ISearchStrategy strategy, bool avoidRepetition = true)
        {
            _strategy = strategy;
            if (avoidRepetition)
                _closed = new List<IState>();
            else
                _closed = null;
        }

        public virtual bool Find(IState initialState)
        {
            _strategy.Add(new Node(initialState));
            while(_strategy.Count() > 0)
            {
                var n = _strategy.Remove();
                if (n.State.IsGoal)
                {
                    CreateSolution(n);
                    return true;
                }
                else
                {
                    foreach (var node in n.Expand(_closed))
                    {
                        _strategy.Add(node);
                        if (_closed != null) _closed.Add(node.State);   
                    }
                }
            }

            return false;
        }

        public void CreateSolution(Node n)
        {
            if (Solution == null) Solution = new List<ISuccessor>();
            var node = n;
            while (!node.IsRoot)
            {
                Solution.Add(node.Successor);
                node = node.Parent;
            }
            Solution.Reverse();
        }

        public List<ISuccessor> Solution { get; set; }
    }
}
