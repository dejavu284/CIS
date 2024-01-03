using System.Net;
using System.Xml.Linq;

namespace CinemaModel
{
    public class Ticket
    {
        public Ticket(int idCinema, Show show, Place place)
        {
            if (idCinema < 0) throw new ArgumentException("id не может быть меньше нуля");
            else
            {
                Show = show;
                IdCinema = idCinema;

                Place = place;
            }
        }
        public string Name { get { return Show.Name; } }
        public DateOnly Date { get { return Show.Date; } }
        public TimeOnly Time { get { return Show.Time; } }
        public Place Place { get; }
        public int Cod { get { return CreateRandomCod(); } }

        private Show Show { get; }

        public int IdCinema { get; }
        public int IdShow { get { return Show.Id; } }

        private static int CreateRandomCod() // хорошо или плохо?
        {
            Random random = new();
            int res = random.Next(1000, 9999);
            return res;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Ticket)
            {
                var item = obj as Ticket;
                if (item == null)
                    return false;
                else if (item.Show.Equals(Show) && item.Place.Equals(Place) && item.IdCinema.Equals(IdCinema))
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
                int hashCode = Show.GetHashCode();
                hashCode = (hashCode * 397) ^ Place.GetHashCode();
                hashCode = (hashCode * 397) ^ IdCinema;
                // Опускаем Seating, так как он не влияет на равенство объектов Show
                return hashCode;
            }
        }
    }

}
