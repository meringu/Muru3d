using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace muru3d
{
    class Model
    {
        private HashSet<Edge> _edges;
        private HashSet<Polygon> _polygons;
        private HashSet<Vertex> _vertices;

        public Model()
        {
            var topLeftFront = new Vertex(new Vector3(-0.5f, 0.5f, -0.5f));
            var topLeftBack = new Vertex(new Vector3(-0.5f, 0.5f, 0.5f));
            var topRightFront = new Vertex(new Vector3(0.5f, 0.5f, -0.5f));
            var topRightBack = new Vertex(new Vector3(0.5f, 0.5f, 0.5f));
            var btmLeftFront = new Vertex(new Vector3(-0.5f, -0.5f, -0.5f));
            var btmLeftBack = new Vertex(new Vector3(-0.5f, -0.5f, 0.5f));
            var btmRightFront = new Vertex(new Vector3(0.5f, -0.5f, -0.5f));
            var btmRightBack = new Vertex(new Vector3(0.5f, -0.5f, 0.5f));

            _edges = new HashSet<Edge>();
            _polygons = new HashSet<Polygon>();
            _vertices = new HashSet<Vertex>
            {
                topLeftFront,
                topLeftBack,
                topRightFront,
                topRightBack,
                btmLeftFront,
                btmLeftBack,
                btmRightFront,
                btmRightBack
            };

            CreateFace(new List<Vertex> { topLeftFront, topRightFront, topRightBack, topLeftBack });
            CreateFace(new List<Vertex> { btmLeftFront, btmLeftBack, btmRightBack, btmRightFront });
            CreateFace(new List<Vertex> { topLeftFront, btmLeftFront, btmRightFront, topRightFront });
            CreateFace(new List<Vertex> { topLeftBack, topRightBack, btmRightBack, btmLeftBack });
            CreateFace(new List<Vertex> { topLeftFront, topLeftBack, btmLeftBack, btmLeftFront });
            CreateFace(new List<Vertex> { topRightFront, btmRightFront, btmRightBack, topRightBack });
        }

        public Polygon CreateFace(List<Vertex> vertices)
        {
            var polygon = new Polygon { Vertices = vertices };
            _polygons.Add(polygon);

            var edges = new List<Edge>();

            for (var i = 0; i < vertices.Count; i++)
            {
                var v1 = vertices[i];
                var v2 = vertices[(i + 1) % vertices.Count];

                var edge = v1.FindOrCreateEdge(v2);

                edge.Faces.Add(polygon);
            }

            polygon.Edges = edges;

            return polygon;
        }

        public void Draw(GraphicsDevice graphicsDevice, BasicEffect basicEffect)
        {
            foreach (var polygon in _polygons)
            {
                polygon.Draw(graphicsDevice, basicEffect);
            }
        }
    }
}
