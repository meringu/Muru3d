using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Muru3D
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

        public void Draw(GraphicsDevice graphicsDevice, BasicEffect basicEffect)
        {

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
