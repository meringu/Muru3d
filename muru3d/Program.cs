using System;

namespace Muru3D
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var muru3D = new Muru3D())
                muru3D.Run();
        }
    }
}
