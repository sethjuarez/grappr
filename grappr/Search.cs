using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class StateExpansionEventArgs : EventArgs
    {
        public StateExpansionEventArgs(IState parent, ISuccessor successor)
        {
            Successor = successor;
            Parent = parent;
            CancelExpansion = false;
        }

        public bool CancelExpansion { get; set; }
        public IState Parent { get; private set; }
        public ISuccessor Successor { get; private set; }
    }

    public class Search
    {
        public event EventHandler<StateExpansionEventArgs> SuccessorExpanded;
        protected virtual void OnSuccessorExpanded(object sender, StateExpansionEventArgs e)
        {
            EventHandler<StateExpansionEventArgs> handler = SuccessorExpanded;
            if (handler != null)
                handler(sender, e);
        }

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
            while (_strategy.Count() > 0)
            {
                var n = _strategy.Remove();
                if (n.Parent != null && n.Successor != null)
                {
                    var eventArgs = new StateExpansionEventArgs(n.Parent.State, n.Successor);
                    OnSuccessorExpanded(this, eventArgs);

                    if (eventArgs.CancelExpansion)
                        return false;
                }

                if (n.State.IsGoal)
                {
                    CreateSolution(n);
                    return true;
                }

                foreach (var node in n.Expand(_closed))
                {
                    _strategy.Add(node);
                    if (_closed != null) _closed.Add(node.State);
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
