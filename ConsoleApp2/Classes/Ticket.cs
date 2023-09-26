using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Ticket
    {
        public Ticket(Film_screening filmScreening)
        {
            this.filmScreening = filmScreening;
            cod = "";
        }
        public Film_screening filmScreening { get; set; }
        public string cod { get; set; }
    }
}
