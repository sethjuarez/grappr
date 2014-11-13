using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public interface IState
    {
        IEnumerable<ISuccessor> Successors { get; }
        bool IsGoal { get; }
        bool IsEqualTo(IState state);
    }

}
