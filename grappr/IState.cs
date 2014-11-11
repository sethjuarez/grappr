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
}
