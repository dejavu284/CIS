using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes.Screen
{
    internal class Screen_2D : IScreen
    {
        public Screen_2D()
        {
            type = "2D";
            coefficient = 1.1;
        }

        public string type { get; }
        public double coefficient { get; }
    }
}
