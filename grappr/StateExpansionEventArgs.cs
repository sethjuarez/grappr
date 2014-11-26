using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class StateExpansionEventArgs : EventArgs
    {

        public StateExpansionEventArgs(IState parent, ISuccessor successor, double cost, int depth)
        {
            Depth = depth;
            Cost = cost;
            Successor = successor;
            Parent = parent;
            CancelExpansion = false;
                    
        }
        public bool CancelExpansion { get; set; }
        public IState Parent { get; private set; }
        public ISuccessor Successor { get; private set; }
        public double Cost { get; private set; }
        public int Depth { get; private set; }
    }
}
