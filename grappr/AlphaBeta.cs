using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class AlphaBeta : AdversarialSearch
    {
        public AlphaBeta()
        {
            Depth = 3;
        }

        public override ISuccessor Find(IAdversarialState state)
        {
            Root = new Node(state);
            Node a;

            if (state.Player)
                a = Max(Root, double.NegativeInfinity, double.PositiveInfinity);
            else
                a = Min(Root, double.NegativeInfinity, double.PositiveInfinity);

            return a.Successor;
        }

        public Node Max(Node node, double alpha, double beta)
        {
            if (IsTerminal(node)) return node;

            double v = double.NegativeInfinity;

            foreach (var successor in (node.State as IAdversarialState).GetSuccessors())
            {
                if (!ProcessEvent(node, successor)) return node;

                var s = successor.State as IAdversarialState;
                var child = new Node(node, successor) { Cost = s.Utility, Depth = node.Depth + 1 };
                node.AddChild(child);


                var g = Min(child, alpha, beta);
                v = Math.Max(v, g.Cost);
                child.Cost = g.Cost;
                node.Cost = v;

                // check to see if we can prune
                if (v >= beta) return child;
                alpha = Math.Max(alpha, v);
            }

            return GetBestChildNode(node, v);
        }

        public Node Min(Node node, double alpha, double beta)
        {
            if (IsTerminal(node)) return node;

            double v = double.PositiveInfinity;

            foreach (var successor in (node.State as IAdversarialState).GetSuccessors())
            {
                if (!ProcessEvent(node, successor)) return node;

                var s = successor.State as IAdversarialState;
                var child = new Node(node, successor) { Cost = s.Utility, Depth = node.Depth + 1 };
                node.AddChild(child);

                var g = Max(child, alpha, beta);
                v = Math.Min(v, g.Cost);
                child.Cost = g.Cost;
                node.Cost = v;

                // check to see if we can prune
                if (v <= alpha) return child;
                beta = Math.Min(beta, v);
            }

            return GetBestChildNode(node, v);
        }
    }
}
