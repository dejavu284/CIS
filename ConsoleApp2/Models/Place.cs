using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIS.Models
{
    internal class Place
    {
        public Place(int number,int row,int colum,int price, bool feedom)
        {
            Number = number;
            Row = row;
            Colum = colum;
            Price = price;
            Freedom = feedom;
        }

        public int Price { get; }
        public int Row { get; }
        public int Colum { get; }
        public bool Freedom { get; set; }
        public int Number { get; }
    }
}
