using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public class AStarSearch : HeuristicSearch
    {
        public override void Add(Node node)
        {
            if (Heuristic == null)
                throw new InvalidOperationException("Invalid Heuristic!");

            var h = node.Cost + Heuristic(node.State);
            Add(node, h);
        }
    }
}
