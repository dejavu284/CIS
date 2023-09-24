using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film_screening
    {
        public Film_screening(Film film, DateOnly data, TimeOnly time)
        {
            this.film = film;
            this.data = data;
            this.time = time;
        }
        public Film film;
        public DateOnly data;
        public TimeOnly time;
    }
}
