using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grappr
{
    public interface ISuccessor
    {
        double Cost { get; }
        string Action { get; }
        IState State { get; }
        bool IsLegal { get; }
    }

    public interface IState
    {
        IEnumerable<ISuccessor> Successors { get; }
        bool IsGoal { get; }
    }

    public abstract class Search
    {
        public abstract bool Find(IState initialState);

        public IEnumerable<Node> Expand(Node node)
        {
            var state = node.State;
            foreach (var successor in state.Successors)
            {
                Node n = new Node
                {
                    Cost = node.Cost + successor.Cost,
                    Depth = node.Depth++,
                    State = successor.State
                };

                Edge.Create(node, n, successor);

                yield return n;
            }
        }


        public Node Path { get; set; }
    }

    public class BreadthFirstSearch : Search
    {
        readonly Queue<Node> _list;
        public BreadthFirstSearch()
        {
            _list = new Queue<Node>();
        }

        public override bool Find(IState initialState)
        {
            _list.Enqueue(new Node { State = initialState, Cost = 0, Depth = 0 });
            while(_list.Count > 0)
            {
                var n = _list.Dequeue();
                if (n.State.IsGoal)
                {
                    Path = n;
                    return true;
                }
                else
                    foreach (var node in Expand(n))
                        _list.Enqueue(node);
            }
            return false;
        }
    }

}
