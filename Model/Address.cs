using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaModel
{
    public class Address
    {
        public Address(string street, string numberHouse, PointF coordinates)
        {
            if (string.IsNullOrEmpty(street)) throw new ArgumentException("Улица не может быть пустой");
            else if (string.IsNullOrEmpty(numberHouse)) throw new ArgumentException("Номер дома не может быть пустой");
            else
            {
                Street = street;
                NumberHouse = numberHouse;
                Coordinates = coordinates;
            }
        }

        public string Street { get; }
        public string NumberHouse { get; }
        public PointF Coordinates { get; }
    }

}
