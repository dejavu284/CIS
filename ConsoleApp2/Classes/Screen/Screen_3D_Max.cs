using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes.Screen
{
    internal class Screen_3D_Max : Screenn
    {
        public Screen_3D_Max()
        {
            type = "3D Max";
            coefficient = 1.5;
        }

        public string type { get; }
        public double coefficient { get; }
    }
}
