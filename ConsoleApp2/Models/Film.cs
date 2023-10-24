using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CIS.Views;

namespace CIS.Models
{
    internal class Film
    {
        public Film(string name, string genre, string description, int year)
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
        public void MessageInfo()
        {
            Console.WriteLine("\nИнформация о фильме {0}", Name);
            Console.WriteLine("\nЖанр: {0}", Genre);
            Console.WriteLine("Год выхода: {0}", Year);
            Console.WriteLine("Описание: {0}\n", Description);
        }
        public static Film ChooseFilm(List<Film> films)//повтор методов вопрос что делать
        {
            return ConsoleMessages.ChooseEl(films);
        }
    }
}
