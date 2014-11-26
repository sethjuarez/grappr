using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class Minimax : AdversarialSearch
    {
        public Minimax()
        {
            Depth = 3;    
        }

        public override ISuccessor Find(IAdversarialState state)
        {
            Root = new Node(state);
            Node a;

            if (state.Player)
                a = Max(Root);
            else
                a = Min(Root);

            return a.Successor;
        }

        private Node Find(Node node, double initial, Func<double, double, double> limit, Func<Node, Node> f)
        {
            if (IsTerminal(node)) return node;

            double v = initial;

            foreach (var successor in (node.State as IAdversarialState).GetSuccessors())
            {
                if (!ProcessEvent(node, successor)) return node;

                var s = successor.State as IAdversarialState;
                var child = new Node(node, successor) { Cost = s.Utility, Depth = node.Depth + 1 };
                node.AddChild(child);

                var g = f(child);
                v = limit(v, g.Cost);
                child.Cost = g.Cost;
                node.Cost = v;
            }

            return GetBestChildNode(node, v);
        }

        public virtual Node Max(Node node)
        {
            return Find(node, double.NegativeInfinity, Math.Max, Min);
        }

        public virtual Node Min(Node node)
        {
            return Find(node, double.PositiveInfinity, Math.Min, Max);
        }
    }
}
