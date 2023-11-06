using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIS.Models
{
    internal class Place
    {
        public Place(int id,int row,int colum,int price, bool freedom)
        {
            Id = id;
            Row = row;
            Colum = colum;
            Price = price;
            Freedom = freedom;
        }

        public int Price { get; }
        public int Row { get; }
        public int Colum { get; }
        public bool Freedom { get; set; }
        public int Id { get; }
    }
}
