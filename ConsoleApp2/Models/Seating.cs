using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Models
{
    internal class Seating
    {
        public Seating(List<Place> places, int idHall)
        {
            Places = places;
            IdHall = idHall;
        }
        public int CountAvailablePlaces { get { return CalcCountAvailablePlaces(); } }
        List<Place> Places{ get; }
        int IdHall { get; }
        private int CalcCountAvailablePlaces()
        {
            int countAvailablePlaces = 0;
            for (int i = 0; i < Places.Count; i++)
            {
                if (Places[i].Freedom)
                {
                    countAvailablePlaces += 1;
                }
            }
            return countAvailablePlaces;
        }

    }
}
