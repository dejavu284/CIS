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
