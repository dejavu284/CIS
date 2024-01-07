using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCIS.Model
{
    public class PlaseInView
    {
        public PlaseInView(int number, int? prise, int numberRow, int numberColum, bool free)
        {
            Number = number;
            Prise = prise;
            NumberRow = numberRow;
            NumberColum = numberColum;
            Free = free;
        }

        public int Number { get; }
        public int? Prise { get; }
        public int NumberRow { get; }
        public int NumberColum {get;}
        public bool Free { get; }
    }
}
