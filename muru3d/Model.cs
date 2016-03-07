using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace muru3d
{
    class Model
    {
        protected HashSet<Polygon> _polygons;

        public Model()
        {
            var topLeftFront  = new Vector3(-0.5f,  0.5f, -0.5f);
            var topLeftBack   = new Vector3(-0.5f,  0.5f,  0.5f);
            var topRightFront = new Vector3( 0.5f,  0.5f, -0.5f);
            var topRightBack  = new Vector3( 0.5f,  0.5f,  0.5f);
            var btmLeftFront  = new Vector3(-0.5f, -0.5f, -0.5f);
            var btmLeftBack   = new Vector3(-0.5f, -0.5f,  0.5f);
            var btmRightFront = new Vector3( 0.5f, -0.5f, -0.5f);
            var btmRightBack  = new Vector3( 0.5f, -0.5f,  0.5f);

            _polygons = new HashSet<Polygon>
            {
                new Polygon { Vertices = new List<Vector3> { topLeftFront,  topRightFront, topRightBack,  topLeftBack   } },
                new Polygon { Vertices = new List<Vector3> { btmLeftFront,  btmLeftBack,   btmRightBack,  btmRightFront } },
                new Polygon { Vertices = new List<Vector3> { topLeftFront,  btmLeftFront,  btmRightFront, topRightFront } },
                new Polygon { Vertices = new List<Vector3> { topLeftBack,   topRightBack,  btmRightBack,  btmLeftBack   } },
                new Polygon { Vertices = new List<Vector3> { topLeftFront,  topLeftBack,   btmLeftBack,   btmLeftFront  } },
                new Polygon { Vertices = new List<Vector3> { topRightFront, btmRightFront, btmRightBack,  topRightBack  } }
            };
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
