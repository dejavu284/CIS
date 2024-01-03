using System.Xml.Linq;

namespace CinemaModel
{
    public class Place
    {
        public Place(int row, int colum, int price)
        {
            if (row<0) throw new ArgumentException("Номер ряда не может быть отрицательным");
            else if (colum<0) throw new ArgumentException("Номер столбца не может быть отрицательным");
            else if (price<=0) throw new ArgumentException("Стоимость не может быть меньше или равна нулю");
            else {
                Row = row;
                Colum = colum;
                Price = price;
            }
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
            unchecked
            {
                // Комбинируем хэш-коды всех значимых свойств для получения уникального хэш-кода
                int hashCode = Row;
                hashCode = (hashCode * 397) ^ Colum.GetHashCode();
                hashCode = (hashCode * 397) ^ Price;
                // Опускаем Seating, так как он не влияет на равенство объектов Show
                return hashCode;
            }
        }
    }
}
