using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Models
{
    internal class Cinema
    {
        public Cinema(List<FilmScreening> film_Screenings, string name)
        {
            this.film_Screenings = film_Screenings;
            this.name = name;
        }
        public List<FilmScreening> film_Screenings { get; set; }
        public string name;
    }
}
