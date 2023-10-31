using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIS.Views;

namespace CIS.Models
{
    internal class Poster
    {
        public Poster(List<Film> films)
        {
            Films = films;
        }
        public List<Film> Films { get; }
        public int Count { get { return Films.Count; } }
    }
}
