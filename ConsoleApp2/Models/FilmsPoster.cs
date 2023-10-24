using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIS.Views;

namespace CIS.Models
{
    internal class FilmsPoster
    {
        public FilmsPoster() { }
        public FilmsPoster(List<Film> films)
        {
            Films = films;
            Count = films.Count;
        }
        public List<Film> Films { get; }
        public int Count { get; private set; }
        public static Film ChooseFilm(List<Film> films)
        {
            return ConsoleMessages.ChooseEl(films);
        }
    }
}
