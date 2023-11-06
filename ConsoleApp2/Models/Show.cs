﻿using System.Text.Json.Serialization;

namespace CIS.Models
{
    
    internal class Show
    {
        public Show(string name, DateOnly date, TimeOnly time, Seating seating)
        {
            Name = name;
            Date = date;
            Time = time;
            Seating = seating;
        }

        [JsonPropertyName("Seating")]
        public Seating Seating { get; }

        [JsonPropertyName("name")] // Эти свойства нужно убрать
        public string Name { get;private set; }

        [JsonPropertyName("data")]
        public DateOnly Date { get;private set; }

        [JsonPropertyName("time")]
        public TimeOnly Time { get; private set; }

        [JsonPropertyName("price")]
        public int Price { get; private set; }
    }
}