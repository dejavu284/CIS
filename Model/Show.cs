using System.Text.Json.Serialization;

namespace CinemaModel
{

    public class Show
    {
        public Show(string name, DateOnly date, TimeOnly time, Seating seating, int id)
        {
            Name = name;
            Date = date;
            Time = time;
            Seating = seating;
            Id = id;
        }

        [JsonPropertyName("Seating")]
        public Seating Seating { get; }

        [JsonPropertyName("name")] 
        public string Name { get; private set; }

        [JsonPropertyName("data")]
        public DateOnly Date { get; private set; }

        [JsonPropertyName("time")]
        public TimeOnly Time { get; private set; }

        [JsonPropertyName("price")]
        public int Price { get; private set; }

        [JsonPropertyName("id")]
        public int Id { get; private set; }
        public void BookingPlaces(Place place)
        {
            Seating.BookingPlace(place);
        }
    }

}
