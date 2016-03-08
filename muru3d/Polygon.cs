using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace muru3d
{
    class Polygon
    {
        public List<Edge> Edges { get; set; }
        public List<Vertex> Vertices { get; set; }

        private Vector3? _center = null;
        private Vector3? _normal = null;
        private VertexPositionNormalTexture[] _triangleList = null;

        public Vector3 Center()
        {
            if (_center.HasValue)
            {
                return _center.Value;
            }

            _center = Vertices.Select(x => x.Position).Aggregate((y, z) => y + z) / Vertices.Count;
            return _center.Value;
        }

        public void Draw(GraphicsDevice graphicsDevice, BasicEffect basicEffect)
        {          
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, TriangleList(), 0, Vertices.Count, VertexPositionNormalTexture.VertexDeclaration);
            }
        }

        public Vector3 Normal()
        {
            if (_normal.HasValue)
            {
                return _normal.Value;
            }

            var normal = Vector3.Zero;

            for (int i = 0; i < Vertices.Count; i++)
            {
                var p1 = Vertices[i].Position;
                var p2 = Vertices[(i + 1) % Vertices.Count].Position;
                var p3 = Vertices[(i + 2) % Vertices.Count].Position;

                var u = p2 - p1;
                var v = p3 - p1;

                var n = u * v;

                if (n.Length() > 0)
                {
                    n.Normalize();
                    normal += n;
                }
            }

            normal.Normalize();
            _normal = normal;
            return normal;
        }

        private VertexPositionNormalTexture[] TriangleList()
        {
            if (_triangleList != null)
            {
                return _triangleList;
            }

            VertexPositionNormalTexture[] triangleList = new VertexPositionNormalTexture[Vertices.Count * 3];

            var center = Center();
            var normal = Normal();

            for (var i = 0; i < Vertices.Count; i++)
            {
                var triangleIndex = i * 3;

                triangleList[triangleIndex] = new VertexPositionNormalTexture(center, normal, Vector2.Zero);
                triangleList[triangleIndex + 1] = new VertexPositionNormalTexture(Vertices[i].Position, normal, Vector2.Zero);
                triangleList[triangleIndex + 2] = new VertexPositionNormalTexture(Vertices[(i + 1) % Vertices.Count].Position, normal, Vector2.Zero);
            }

            _triangleList = triangleList;
            return triangleList;
        }
    }
}
