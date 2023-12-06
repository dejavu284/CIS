using System.Text.Json.Serialization;

namespace CinemaModel
{
    public class Film
    {
        public Film(string name, string genre, string description, int year) 
        {
            if (year <= 1894)
                throw new Exception("Не корректный год сооздания фильма. Год создания должен быть больше 1894.");
            else if (string.IsNullOrEmpty(name))
                throw new Exception("Не корректное название фильма. Название фильма не может быть пустым.");
            else if (string.IsNullOrEmpty(genre))
                throw new Exception("Не корректный жанр фильма. Название жанра не может быть пустым.");
            else
            {
                Name = name;
                Description = description;
                Genre = genre;
                Year = year;
            }
            
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
