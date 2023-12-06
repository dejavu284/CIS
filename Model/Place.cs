using System.Xml.Linq;

namespace CinemaModel
{
    public class Place
    {
        public Place(int row, int colum, int price)
        {
            Row = row;
            Colum = colum;
            Price = price;
        }

        public int Price { get; }
        public int Row { get; }
        public int Colum { get; }

        public override bool Equals(object? obj)
        {
            if (obj is Place)
            {
                var item = obj as Place;
                if (item == null)
                    return false;
                else if (item.Row.Equals(Row) && item.Colum.Equals(Colum) && item.Price.Equals(Price))
                    return true;
                else
                    return false;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
