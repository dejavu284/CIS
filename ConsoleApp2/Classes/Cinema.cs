using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Cinema
    {
        public Cinema(List<Film_screening> film_Screenings,string name) 
        {
            this.film_Screenings = film_Screenings;
            this.name = name;
        } 
        public List<Film_screening> film_Screenings { get; set; }
        public string name;
    }
}
