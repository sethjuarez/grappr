using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grappr
{
    public class Graph
    {
        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public string Label { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
    }

    public abstract class Element
    {
        public string Label { get; set; }
        public string Id { get; set; }
    }

    public class Node : Element
    {
        public Node()
        {
            Label = String.Empty;
            Id = Guid.NewGuid().ToString();
            Edges = new List<Edge>();
        }

        public double Cost { get; set; }
        public int Depth { get; set; }
        public IState State { get; set; }
        public List<Edge> Edges { get; set; }
        public override string ToString()
        {
            return string.Format("{0} [{1}, {2}]", Id, Label, Edges.Count());
        }
    }

    public class Edge : Element
    {
        public Edge()
        {
            Label = String.Empty;
            Directed = true;
        }

        public ISuccessor Successor { get; set; }

        public Node Source { get; set; }
        public Node Target { get; set; }
        public bool Directed { get; set; }

        public override string ToString()
        {
            var fmt = Directed ? "{0} -{1}-> {2}" : "{0} -{1}- {2}";
            return string.Format(fmt, Source.Id, Label, Target.Id);
        }

        public static Edge Create(Node source, Node target, ISuccessor successor)
        {
            Edge e = new Edge { Source = source, Target = target, Successor = successor };
            e.Label = successor.Action;
            source.Edges.Add(e);
            target.Edges.Add(e);
            return e;
        }
    }
}
