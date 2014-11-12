using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class Node
    {
        public Node(IState state)
        {
            State = state;
            Parent = null;
            Successor = null;
            Cost = 0;
            Depth = 0;
        }

        public Node(Node parent, ISuccessor successor)
        {
            State = successor.State;
            Parent = parent;
            Successor = successor;
            Cost = parent.Cost + successor.Cost;
            Depth = parent.Depth + 1;
        }

        public Node Parent { get; set; }
        public ISuccessor Successor { get; set; }
        public IState State { get; set; }
        public bool IsRoot { get { return Parent == null && Successor == null; } }

        public double Cost { get; set; }
        public int Depth { get; set; }
        public List<Node> Children { get; set; }
        

        public IEnumerable<Node> Expand()
        {
            if (State == null)
                throw new InvalidOperationException("Invalid node state!");

            Children = new List<Node>();

            foreach (var successor in State.Successors)
                Children.Add(new Node(this, successor));

            return Children;
        }
    }
}
