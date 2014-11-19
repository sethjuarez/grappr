using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class StateExpansionEventArgs : EventArgs
    {

        public StateExpansionEventArgs(IState parent, ISuccessor successor, Node node)
        {
            Successor = successor;
            Parent = parent;
            Node = node;    
            CancelExpansion = false;
                    
        }
        public bool CancelExpansion { get; set; }
        public IState Parent { get; private set; }
        public ISuccessor Successor { get; private set; }
        public Node Node { get; private set; }
    }
}
