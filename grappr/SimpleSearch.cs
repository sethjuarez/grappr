using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class SimpleSearch : Search
    {
        private readonly List<IState> _closed;

        public List<ISuccessor> Solution { get; set; }

        public ISearchStrategy Strategy { get; set; }

        public SimpleSearch(ISearchStrategy strategy, bool avoidRepetition = true)
        {
            Strategy = strategy;
            if (avoidRepetition)
                _closed = new List<IState>();
            else
                _closed = null;
        }

        public virtual bool Find(IState initialState)
        {
            if (Strategy == null) return false;

            Strategy.Add(new Node(initialState));
            while (Strategy.Count() > 0)
            {
                var n = Strategy.Remove();
                if (n.Parent != null && n.Successor != null)
                {
                    var eventArgs = new StateExpansionEventArgs(n.Parent.State, n.Successor, n.Cost, n.Depth);
                    OnSuccessorExpanded(this, eventArgs);

                    if (eventArgs.CancelExpansion)
                        return false;
                }

                if (n.State.IsTerminal)
                {
                    CreateSolution(n);
                    return true;
                }

                foreach (var node in n.Expand(_closed))
                {
                    Strategy.Add(node);
                    if (_closed != null) _closed.Add(node.State);
                }

            }

            return false;
        }

        private void CreateSolution(Node n)
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
    }
}
