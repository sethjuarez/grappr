using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr.Search
{
    public class BreadthFirstSearch : Search
    {
        readonly Queue<Node> _list;
        public BreadthFirstSearch()
        {
            _list = new Queue<Node>();
        }

        public override bool Find(IState initialState)
        {
            _list.Enqueue(new Node(initialState));
            while (_list.Count > 0)
            {
                var n = _list.Dequeue();
                if (n.State.IsGoal)
                {
                    CreateSolution(n);
                    return true;
                }
                else
                    foreach (var node in n.Expand())
                        _list.Enqueue(node);
            }
            return false;
        }
    }
}
