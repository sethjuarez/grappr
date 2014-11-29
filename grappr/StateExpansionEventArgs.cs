using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class StateEventArgs : EventArgs
    {
        public StateEventArgs(IState state)
        {
            State = state;
        }
        public IState State { get; private set; }
    }
    public class StateExpansionEventArgs : StateEventArgs
    {

        public StateExpansionEventArgs(IState parent, ISuccessor successor, double cost, int depth)
            :base (parent)
        {
            Depth = depth;
            Cost = cost;
            Successor = successor;
            CancelExpansion = false;
                    
        }
        public bool CancelExpansion { get; set; }
        public ISuccessor Successor { get; private set; }
        public double Cost { get; private set; }
        public int Depth { get; private set; }
    }
}
