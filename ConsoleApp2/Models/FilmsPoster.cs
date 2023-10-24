using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2.Models
{
    internal class FilmsPoster
    {
        public FilmsPoster(List<Film> films)
        {
            Films = films;
            Count = films.Count;
        }
        public List<Film> Films { get; }
        public int Count { get; private set; }
        public void MessageNamesAllFilms()
        {
            MessageFilmsAtTheBoxOffice();
            for (int i = 0; i < Count; i++)
            {

                Console.WriteLine("{0}. {1}", i + 1, Films[i].Name);
            }
        }
        private void MessageFilmsAtTheBoxOffice()
        {
            Console.WriteLine("Фильмы в прокате:\n");
        }
    }
}
