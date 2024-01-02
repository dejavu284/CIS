using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCIS.Model
{
    public class Seat
    {
        public Seat(int number, int? prise, int numberRow, bool free)
        {
            Number = number;
            Prise = prise;
            NumberRow = numberRow;
            Free = free;
        }

        public int Number { get; }
        public int? Prise { get; }
        public int NumberRow { get; }
        public bool Free { get; }
    }
}
