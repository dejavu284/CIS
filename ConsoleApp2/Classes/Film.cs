using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film
    {
        public Film(string name, string description, List<string> genre, List<string> actors)
        {
            this.name = name;
            this.description = description;
            this.genre = genre;
            this.actors = actors;
        }


        public string name { get; }
        public string description { get; }
        public List<string> actors { get; }
        public List<string> genre { get; }


    }
}
