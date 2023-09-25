using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film
    {
        public Film(string name, string genre, string description, int year)
        {
            this.name = name;
            this.description = description;
            this.genre = genre;
            this.year = year;
            
        }
        public string name { get; }
        public string description { get; }
        public string genre { get; }
        public int year { get; }
    }
}
