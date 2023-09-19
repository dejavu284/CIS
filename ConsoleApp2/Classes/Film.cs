using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film
    {
        /*public Film(string name,string description, List<string> genre, List<string> actors) {
            this.name = name;
            this.description = description; 
            this.genre = genre;
            this.actors = actors;
        }*/


        public string name { get; set; }
        public string description { get; set; }
        public List<string> actors { get; set; }
        public List<string> genre { get; set; }


    }
}
