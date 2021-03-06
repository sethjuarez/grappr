﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public interface IState
    {
        string Id { get; }
        bool IsTerminal { get; }
        bool IsEqualTo(IState state);
        IEnumerable<ISuccessor> GetSuccessors();
        double Heuristic();
    }
}