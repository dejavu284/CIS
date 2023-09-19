using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film
    {
        public Film(string name, string description, string genre, int year) {
            this.name = name;
            this.description = description;
            this.genre = genre;
            this.year = year;
        }
        private string name;
        private string description;
        private string genre;
        private int year;

        public string Name { get { return name; } }

    }
}
