using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grappr
{    
    public class Minimax : AdversarialSearch
    {
        public int Depth { get; set; }
        public override ISuccessor Find(IAdversarialState state)
        {
            double value = 0;

            
            if (state.Player)
                value = Max(state, Depth * 2);
            else
                value = Min(state, Depth * 2);

            return null;
        }



        public double Max(IAdversarialState state, int depth)
        {
            if (depth - 1 == 0) return state.Utility;
            double v = double.NegativeInfinity;
            foreach (var successor in state.GetSuccessors())
            {
                var s = successor.State as IAdversarialState;
                if (ProcessEvent(state, depth, successor))
                    v = Math.Max(v, Min(s, depth - 1));
                else return s.Utility;
            }
            return v;
        }

        public double Min(IAdversarialState state, int depth)
        {
            if (depth - 1 == 0) return state.Utility;
            double v = double.PositiveInfinity;
            foreach (var successor in state.GetSuccessors())
            {
                var s = successor.State as IAdversarialState;
                if (ProcessEvent(state, depth, successor))
                    v = Math.Max(v, Min(s, depth - 1));
                else return s.Utility;
            }
            return 0;
        }

        private bool ProcessEvent(IAdversarialState state, int depth, ISuccessor successor)
        {
            var args = new StateExpansionEventArgs(state, successor, state.Utility, depth);
            OnSuccessorExpanded(this, args);
            return !args.CancelExpansion;
        }
    }
}
