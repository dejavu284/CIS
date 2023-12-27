using System.Text.Json.Serialization;

namespace CinemaModel
{

    public class Show
    {
        public Show(string name, DateOnly date, TimeOnly time, Seating seating, int id)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Имя фильма должно быть не пустым");
            else if (id < 0) throw new ArgumentException("id не может быть меньше нуля");
            else
            {
                Name = name;
                Date = date;
                Time = time;
                Seating = seating;
                Id = id;
            }
        }

        [JsonPropertyName("Seating")]
        public Seating Seating { get; }

        [JsonPropertyName("name")] 
        public string Name { get; private set; }

        [JsonPropertyName("data")]
        public DateOnly Date { get; private set; }

        [JsonPropertyName("time")]
        public TimeOnly Time { get; private set; }

        [JsonPropertyName("id")]
        public int Id { get; private set; }
        public void BookingPlaces(Place place)
        {
            Seating.BookingPlace(place);
        }
        public override bool Equals(object? obj)
        {
            if (obj is Show)
            {
                var item = obj as Show;
                if (item == null)
                    return false;
                else if (item.Name.Equals(Name) && item.Date.Equals(Date) && item.Time.Equals(Time) && item.Id.Equals(Id))
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
