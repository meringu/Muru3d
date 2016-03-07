using System;

namespace muru3d
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var muru3d = new Muru3d())
                muru3d.Run();
        }
    }
}
