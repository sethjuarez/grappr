using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public interface ISuccessor
    {
        double Cost { get; }
        string Action { get; }
        IState State { get; }
        //bool IsLegal { get; }
    }
}
