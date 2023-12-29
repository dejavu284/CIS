using System.ComponentModel.Design;
using System.Numerics;

namespace CinemaModel
{
    public class Seating
    {
        public Seating(int[][] places, int idHall)
        {
            if (idHall < 0) throw new ArgumentException("id не может быть меньше нуля");
            else if (!CheckingNullRow(places)) throw new ArgumentException("рассадка не должна быть пустой");
            else if (!CheckingDifferentCountRow(places)) throw new ArgumentException("вместимость всех рядов должна быть одинакова");
            else if (!CheckingPositivNumberPlase(places)) throw new ArgumentException("стоимость мест должна быть положительна или место должно быть занято");
            else {
                Places = places;
                IdHall = idHall;
            }
        }

        

        public int CountAvailableSeats { get { return CalcCountAvailablePlaces(); } }
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
        private bool CheckingDifferentCountRow(int[][] layout)
        {
            for (int i = 1; i < layout.Length; i++)
            {
                if (layout[i - 1].Length != layout[i].Length)
                    return false;
            }
            return true;
        }
        private bool CheckingPositivNumberPlase(int[][] layout)
        {
            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    if (layout[i][j] <= 0 && layout[i][j] != -1)
                        return false;
                }
            }
            return true;
        }
        private bool CheckingNullRow(int[][] layout)
        {
            if(layout.Length == 0) return false;
            for (int i = 0; i < layout.Length; i++)
            {
                if (layout[i] == null || layout[i].Length == 0)
                    return false;
            }
            return true;
        }
    }
}

