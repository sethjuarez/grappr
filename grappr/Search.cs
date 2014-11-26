using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public abstract class Search
    {
        public event EventHandler<StateExpansionEventArgs> SuccessorExpanded;
        protected virtual void OnSuccessorExpanded(object sender, StateExpansionEventArgs e)
        {
            EventHandler<StateExpansionEventArgs> handler = SuccessorExpanded;
            if (handler != null)
                handler(sender, e);
        }
    }

    public abstract class AdversarialSearch : Search
    {
        public int Depth { get; set; }
        public Node Root { get; set; }

        internal Node GetBestChildNode(Node node, double v)
        {
            var q = node.Children
                        .Where(n => n.Cost == v && n.State.IsTerminal);

            Node r;
            if (q.Count() > 0) // favor terminal nodes first
                r = q.Rand();
            else
                r = node.Children
                            .Where(n => n.Cost == v)
                            .Rand();
            r.Cost = v;
            r.Path = true;
            return r;
        }

        internal bool IsTerminal(Node node)
        {
            return (node.State as IAdversarialState).IsTerminal ||
                    node.Depth == Depth * 2;
        }

        internal bool ProcessEvent(Node parentNode, ISuccessor successor)
        {
            var state = parentNode.State as IAdversarialState;
            var args = new StateExpansionEventArgs(state, successor, parentNode.Cost, parentNode.Depth);
            OnSuccessorExpanded(this, args);
            return !args.CancelExpansion;
        }

        public abstract ISuccessor Find(IAdversarialState state);
    }

}