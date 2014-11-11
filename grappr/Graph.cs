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


        public Node AddNode(string id)
        {
            var q = Nodes.Where(n => n.Id == id);
            if (q.Count() == 0)
            {
                var n = new Node { Id = id };
                Nodes.Add(n);
                return n;
            }
            else
                return q.First();
        }

        public Edge AddEdge(string fromId, string toId)
        {
            var q = Edges.Where(e => e.Source.Id == fromId && e.Target.Id == toId);
            if (q.Count() == 0)
            {
                var e = Edge.Create(AddNode(fromId), AddNode(toId));
                Edges.Add(e);
                return e;
            }
            else
                return q.First();
        }
    }

    public abstract class Element
    {
        public string Label { get; set; }
        public string Id { get; set; }
        public Dictionary<string, string> Props { get; set; }
    }

    public class Node : Element
    {
        public Node()
        {
            Label = String.Empty;
            Id = String.Empty;
            Edges = new List<Edge>();
            Props = new Dictionary<string, string>();
        }

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
            Props = new Dictionary<string, string>();
        }

        public Node Source { get; set; }
        public Node Target { get; set; }
        public bool Directed { get; set; }

        public override string ToString()
        {
            var fmt = Directed ? "{0} -{1}-> {2}" : "{0} -{1}- {2}";
            return string.Format(fmt, Source.Id, Label, Target.Id);
        }

        public static Edge Create(Node source, Node target)
        {
            Edge e = new Edge { Source = source, Target = target };
            source.Edges.Add(e);
            target.Edges.Add(e);
            return e;
        }
    }
}
