using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film
    {
        public Film(string name, string description, string genre, int year, Dictionary<DateTime, double> timeAndCost, DateTime date)
        {
            this.name = name;
            this.description = description;
            this.genre = genre;
            this.year = year;
            this.timeAndCost = timeAndCost;
            this.date = date;
        }


        public string name { get; }
        public string description { get; }
        public string genre { get; }
        public int year { get; }
        public Dictionary<DateTime, double> timeAndCost { get; } 
        public DateTime date { get; }
      
    }
}
