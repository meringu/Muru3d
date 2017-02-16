using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muru3D
{
    class Edge
    {
        public List<Polygon> Faces { get; set; }
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }

        public Edge(Vertex v1, Vertex v2)
        {
            V1 = v1;
            V2 = v2;
            Faces = new List<Polygon>();
        }

        public void Draw(GraphicsDevice graphicsDevice, BasicEffect basicEffect)
        {
            var lineBasicEffect = new BasicEffect(graphicsDevice);

            lineBasicEffect.World = basicEffect.World;
            lineBasicEffect.View = basicEffect.View;
            lineBasicEffect.Projection = basicEffect.Projection;

            lineBasicEffect.DiffuseColor = Vector3.Zero;

            var lineList = new VertexPosition[]
            {
                new VertexPosition(V1.Position), new VertexPosition(V2.Position)
            };

            foreach (EffectPass pass in lineBasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lineList, 0, 1, VertexPosition.VertexDeclaration);
            }
        }

        public Vertex OtherVertex(Vertex vertex)
        {
            if (V1 == vertex)
            {
                return V2;
            }
            else if (V2 == vertex)
            {
                return V1;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
