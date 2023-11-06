using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CIS.Views;

namespace CIS.Models
{
    
    internal class Show
    {
        public Show(string name, DateOnly date, TimeOnly time, int countTicket, int price)
        {
            Name = name;
            Date = date;
            Time = time;
            CountTicket = countTicket;
            Price = price;
        }
        
        [JsonPropertyName("name")]
        public string Name { get;private set; }
        [JsonPropertyName("data")]
        public DateOnly Date { get;private set; }
        [JsonPropertyName("time")]
        public TimeOnly Time { get; private set; }
        [JsonPropertyName("countTiket")]
        public int CountTicket { get; private set; }
        [JsonPropertyName("price")]
        public int Price { get; private set; }
        public bool IsPlacesNotEmpty()
        {
            return CountTicket != 0;
        }
    }
}
