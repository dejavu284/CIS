using System.Text.Json.Serialization;

namespace CinemaModel
{
    public class Film
    {
        public Film(string name, string genre, string description, int year) // На этот конструктор 0 ссылок
        {
            Name = name;
            Description = description;
            Genre = genre;
            Year = year;
        }
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("description")]
        public string Description { get; }

        [JsonPropertyName("genre")]
        public string Genre { get; }

        [JsonPropertyName("year")]
        public int Year { get; }
    }
}
