using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public interface IAdversarialState : IState
    {
        double Utility { get; }
        bool Player { get; }
        IAdversarialState Reset();
    }
}
