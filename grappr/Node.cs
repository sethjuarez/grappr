using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace grappr
{
    public class Node
    {
        private readonly StateComparer _stateComparer;
        public Node(IState state)
        {
            State = state;
            Parent = null;
            Successor = null;
            Cost = 0;
            Depth = 0;
            Path = false;
            Children = new List<Node>();
            _stateComparer = new StateComparer();
        }

        public Node(Node parent, ISuccessor successor)
        {
            State = successor.State;
            Parent = parent;
            Successor = successor;
            Cost = parent.Cost + successor.Cost;
            Depth = parent.Depth + 1;
            Path = false;
            Children = new List<Node>();
            _stateComparer = new StateComparer();
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Node Parent { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ISuccessor Successor { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IState State { get; set; }
        public bool IsRoot { get { return Parent == null && Successor == null; } }
        public bool Path { get; set; }

        public double Cost { get; set; }
        public int Depth { get; set; }
        public List<Node> Children { get; set; }

        public IEnumerable<Node> Expand() { return Expand(null); }

        public IEnumerable<Node> Expand(IEnumerable<IState> closed)
        {
            foreach (var successor in State.GetSuccessors())
                if (closed == null || !closed.Contains(successor.State, _stateComparer))
                    AddChild(new Node(this, successor));

            return Children;
        }

        public void AddChild(Node n)
        {
            if (State == null)
                throw new InvalidOperationException("Invalid node state!");

            Children.Add(n);
        }
    }
}
