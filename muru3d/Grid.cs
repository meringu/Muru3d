using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace muru3d
{
    class Grid
    {
        public Camera Camera { get; set; }

        private VertexPosition[] _vertices;

        public Grid(Camera camera)
        {
            Camera = camera;

            List<VertexPosition> vertices = new List<VertexPosition>();

            foreach (float x in Enumerable.Range(-10, 21))
            {
                vertices.Add(new VertexPosition(new Vector3(x / 10f , 0, -1f)));
                vertices.Add(new VertexPosition(new Vector3(x / 10f, 0, 1f)));
            }

            foreach (float z in Enumerable.Range(-10, 21))
            {
                vertices.Add(new VertexPosition(new Vector3(-1f, 0, z / 10f)));
                vertices.Add(new VertexPosition(new Vector3(1f, 0, z / 10f)));
            }

            _vertices = vertices.ToArray();
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            var basicEffect = new BasicEffect(graphicsDevice);

            basicEffect.World = Matrix.Identity;
            basicEffect.View = Camera.ViewMatrix();
            basicEffect.Projection = Camera.ProjectionMatrix(graphicsDevice.Viewport.AspectRatio);

            basicEffect.DiffuseColor = new Vector3(0.3f, 0.3f, 0.3f);

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                
                graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _vertices, 0, _vertices.Length / 2, VertexPosition.VertexDeclaration);
            }
        }
    }
}
