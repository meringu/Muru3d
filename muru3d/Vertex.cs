using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace muru3d
{
    class Vertex
    {
        public List<Edge> Edges { get; set; }
        public Vector3 Position { get; set; }

        public Vertex(Vector3 position)
        {
            Edges = new List<Edge>();
            Position = position;
        }

        public Edge FindOrCreateEdge(Vertex other)
        {
            foreach (var existingEdge in Edges)
            {
                if (existingEdge.OtherVertex(this) == other)
                {
                    return existingEdge;
                }
            }
            var newEdge = new Edge(this, other);
            Edges.Add(newEdge);
            other.Edges.Add(newEdge);
            return newEdge;
        }
    }
}
