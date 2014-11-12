using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr.Search
{
    public abstract class Search
    {
        public abstract bool Find(IState initialState);

        public void CreateSolution(Node n)
        {
            if (Solution == null) Solution = new List<ISuccessor>();
            var node = n;
            while (!node.IsRoot)
            {
                Solution.Add(node.Successor);
                node = node.Parent;
            }
            Solution.Reverse();
        }

        public List<ISuccessor> Solution { get; set; }

    }
}
