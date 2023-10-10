using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Place
    {
        public Place(string type, double coefficient, bool freedom, int number)
        {
            this.type = type;
            this.coefficient = coefficient;
            this.freedom = freedom;
            this.number = number;
        }

        public string type;
        public double coefficient;
        public bool freedom;
        public int number;
    }
}
