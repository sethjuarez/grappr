using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grappr
{
    public static class EnumerableExtensions
    {
        private static Random _random = new Random(DateTime.Now.Millisecond);
        public static T Rand<T>(this IEnumerable<T> items)
        {
            var count = items.Count();
            var d = _random.Next(count);
            return items.ElementAt(d);
        }
    }

    public class Minimax : AdversarialSearch
    {
        public int Depth { get; set; }
        public Node Root { get; set; }
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
            var state = node.State as IAdversarialState;

            if(state.IsTerminal || node.Depth == Depth * 2)
            {
                node.Cost = state.Utility;
                return node;
            }

            double v = initial;
            
            foreach (var successor in state.GetSuccessors())
            {
                var s = successor.State as IAdversarialState;
                var child = new Node(node, successor) { Cost = s.Utility, Depth = node.Depth + 1 };
                node.AddChild(child);

                if (ProcessEvent(node, successor))
                {
                    var g = f(child);
                    v = limit(v, g.Cost);
                    child.Cost = g.Cost;
                    node.Cost = v;
                }
                else return child;
            }

            Console.WriteLine("Best v: {0}", v, f.Method.Name);

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

        public Node Max(Node node)
        {
            var n = Find(node, double.NegativeInfinity, Math.Max, Min);
            return n;
        }

        public Node Min(Node node)
        {
            var n = Find(node, double.PositiveInfinity, Math.Min, Max);
            return n;
        }

        private bool ProcessEvent(Node node, ISuccessor successor)
        {
            var state = node.State as IAdversarialState;
            var args = new StateExpansionEventArgs(state, successor, node.Cost, node.Depth);
            OnSuccessorExpanded(this, args);
            return !args.CancelExpansion;
        }
    }
}
