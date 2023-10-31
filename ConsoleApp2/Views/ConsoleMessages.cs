using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CIS.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CIS.Views
{
    internal class ConsoleMessages
    {
        public static bool PoolYesOrNo(string question)
        {
            string yes = "y";
            string no = "n";
            Console.WriteLine("{0}? ({1}/{2})", question, yes, no);
            while (true)
            {
                string answer = Console.ReadLine()!;
                if (answer!.ToLower() == yes)
                    return true;
                else if (answer.ToLower() == no)
                    return false;
                else
                    MessageIncorrectInput();
            }
        }
        public static void MessageToSelectItemEnterNumber()
        {
            Console.WriteLine("\nДля выбора элемента введите его номер");
            Console.WriteLine();
        }
        public static void MessageIncorrectInput()
        {
            Console.WriteLine("\nНекорректный ввод поробуйте ещё раз");
        }
        public static void MessageCheck(Basket basket)
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Чек:");
            for (int i = 0; i < basket.NumberTickets; i++)
            {
                Console.WriteLine("\nБилет №{0}", i + 1);
                MessangInfo(basket.Tickets[i]);
            }
            Console.WriteLine("\nИтоговая стоимость составила: {0}", basket.Price);
            Console.WriteLine("---------------------------");
        }
        public static void MessangInfo(Ticket ticket)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Информация о билете:");
            Console.WriteLine("Название фильма: {0}", ticket.Name);
            Console.WriteLine("Дата сеанса: {0}", ticket.Data);
            Console.WriteLine("Время сеанса: {0}", ticket.Time);
            Console.WriteLine("Цена билета: {0}", ticket.Price);
            Console.WriteLine("---------------------------\n");
        }
        public static T ChooseEl<T>(List<T> elements)
        {
            MessageToSelectItemEnterNumber();
            do
            {
                string? inputNumber = Console.ReadLine();
                int index;
                if(IsNumberInList(elements.Count, inputNumber, out index))
                    return elements[index - 1];
                else
                    MessageIncorrectInput(); // throw new ArgumentException("Выбранного елемента нет в списке");
            } while (true);
        }
        private static bool IsNumberInList(int count, string? indexStr, out int index)
        {
            bool tryParseChecked = int.TryParse(indexStr, out index);
            return tryParseChecked && count >= index && index > 0;
        }
        public static void MessageTicketPurchased()
        {
            Console.WriteLine("Билет куплен");
        }
        public static void MessageCompletionProgram()
        {
            Console.WriteLine("\nСпасибо за покупку, приходите ещё");
            Console.ReadKey();
        }
        public static void MessageFilmNotExist()
        {
            Console.WriteLine("К сожалению, фильм не идет в кинотеатре\nВыберете другой фильм\n");
        }
        public static void OutputTimeFilmScreening(List<FilmScreening> filmscreenings)
        {
            Console.WriteLine("Время показа фильма:");
            for (int i = 0; i < filmscreenings.Count; i++)
            {
                Console.WriteLine("\n{0}. {1}. Цена: {2} руб.", i + 1, filmscreenings[i].Time, filmscreenings[i].Price);
            }
        }
        public static void OutputCountPlace(FilmScreening filmScreening)
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", filmScreening.CountTicket);
        }
        public static void OutputDateFilmScreening(List<DateOnly> datesFilmScreenings)
        {
            Console.WriteLine("Даты показа фильма:\n");
            for (int i = 0; i < datesFilmScreenings.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, datesFilmScreenings[i]);
            }
            Console.WriteLine();
        }
        public static void MessagePlaceNotExist()
        {
            Console.WriteLine("На выбранное время мест нет.\n");
        }
        public static void MessageInfo(Film film)
        {
            Console.WriteLine("\nИнформация о фильме {0}", film.Name);
            Console.WriteLine("\nЖанр: {0}", film.Genre);
            Console.WriteLine("Год выхода: {0}", film.Year);
            Console.WriteLine("Описание: {0}\n", film.Description);
        }
        public static void MessageNamesAllFilms(Poster films)
        {
            Console.WriteLine("Фильмы в прокате:\n");
            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, films.Films[i].Name);
            }
        }
    }
}
