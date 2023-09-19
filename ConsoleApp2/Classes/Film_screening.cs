using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Film_screening
    {
        public Film_screening(Film film, float data,float time ){
            this.film = film;
            this.data = data;
            this.time = time;
        }
        private Film film;
        private float data;
        private float time;
    }
}
