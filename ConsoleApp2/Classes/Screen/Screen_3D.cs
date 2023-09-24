using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes.Screen
{
    internal class Screen_3D : Screen
    {
        public Screen_3D()
        {
            type = "3D";
            coefficient = 1.3;
        }

        public string type { get; }
        public double coefficient { get; }
    }
}
