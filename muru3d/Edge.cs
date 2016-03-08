using System;
using System.Collections.Generic;

namespace muru3d
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
