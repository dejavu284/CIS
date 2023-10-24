using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CIS.Views;

namespace CIS.Models
{
    internal class FilmScreening
    {
        public List<FilmScreening> filmScreenings = new();
        public FilmScreening() { }
        public FilmScreening(string name, DateOnly data, TimeOnly time, int countTiket, int price)
        {
            Name = name;
            Date = data;
            Time = time;
            CountTicket = countTiket;
            Price = price;
        }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("data")]
        public DateOnly Date { get; set; }
        [JsonPropertyName("time")]
        public TimeOnly Time { get; set; }
        [JsonPropertyName("countTiket")]
        public int CountTicket { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
        public bool IsPlacesNotEmpty()
        {
            return CountTicket != 0;
        }
        public static bool IsDatesEqual(DateOnly date, FilmScreening filmScreening)
        {
            return date == filmScreening.Date;
        }
    }
}
