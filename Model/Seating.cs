namespace CinemaModel
{
    public class Seating
    {
        public Seating(int[][] places, int idHall)
        {
            Places = places;
            IdHall = idHall;
        }
        public int NumberAvailableSeats { get { return CalcCountAvailablePlaces(); } }

        public int[][] Places { get;}

        public int IdHall { get;}
        private int CalcCountAvailablePlaces()
        {
            int countAvailablePlaces = 0;
            for (int i = 0; i < Places.GetLength(0); i++)
            {
                for (int j = 0; j < Places[i].Length; j++)
                {
                    if (Places[i][j] != -1)
                    {
                        countAvailablePlaces += 1;
                    }
                }
            }
            return countAvailablePlaces;
        }
        private bool CheckingPossibilityBooking(Place place)
        {
            bool result;

            if (place == null)
                result = false;
            else if (place.Row >= Places.Length)
                result = false;
            else if (place.Colum >= Places[place.Colum].Length)
                result = false;
            else if (Places[place.Row][place.Colum] == -1)
                result = false;
            else 
                result = true;

            return result;
        }

        public void BookingPlace(Place place)
        {
            if(CheckingPossibilityBooking(place))
            Places[place.Row][place.Colum] = -1;
        }
    }
}
