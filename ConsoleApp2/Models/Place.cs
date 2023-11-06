using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIS.Models
{
    internal class Place
    {
        public Place(int row,int colum,int price)
        {
            Row = row;
            Colum = colum;
            Price = price;
        }

        public int Price { get; }
        public int Row { get; }
        public int Colum { get; }
    }
}
