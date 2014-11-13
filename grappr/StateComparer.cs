using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class StateComparer : IEqualityComparer<IState>
    {

        public bool Equals(IState x, IState y)
        {
            return x.IsEqualTo(y);
        }

        public int GetHashCode(IState obj)
        {
            return obj.GetHashCode();
        }
    }
}
