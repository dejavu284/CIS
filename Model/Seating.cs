using System.Numerics;

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
        public int CountRow { get { return Places.GetLength(0); } }

        public int[][] Places { get; }

        public int IdHall { get; }
        private int CalcCountAvailablePlaces()
        {
            int countAvailablePlaces = 0;
            for (int i = 0; i < Places.GetLength(0); i++)
            {
                if (Places[i] != null)
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
        public void BookingPlace(Place place)
        {
            if (CheckAvailabilityPlace(place.Row + 1, place.Colum + 1))
            {
                Places[place.Row][place.Colum] = -1;
            }
        }
        public bool CheckAvailabilityPlace(int numberRow, int numberColum)
        {
            bool result;

            if (!CheckExistencePlace(numberRow, numberColum))
                result = false;
            else if (Places[numberRow-1][numberColum - 1] == -1)
                result = false;
            else
                result = true;
            return result;
        }
        public bool CheckExistenceRow(int numberRow)
        {
            if (numberRow <= CountRow && numberRow > 0)
                return true;
            else return false;
        }
        public bool CheckAvailabilityRow(int numberRow)
        {
            if (CheckExistenceRow(numberRow))
                for (int j = 0; j < Places[numberRow - 1].Length -1; j++)
                {
                    if (!CheckAvailabilityPlace(numberRow, j+1))
                    {
                        return false;
                    }
                }
            else
                return false;
            return true;
        }
        private bool CheckExistencePlace(int numberRow, int numberColum)
        {
            bool result;
            if (!CheckExistenceRow(numberRow))
                result = false;
            else if (numberColum >= Places[numberRow].Length)
                result = false;
            else
                result = true;
            return result;
        }
    }
}

