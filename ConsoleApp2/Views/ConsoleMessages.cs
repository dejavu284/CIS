using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CIS.Models;

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
                    ConsoleMessages.MessageIncorrectInput();
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
        public static void MessageCheck()
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Чек:");
            for (int i = 0; i < Basket.NumberTickets; i++)
            {
                Console.WriteLine("\nБилет №{0}", i + 1);
                Basket.Tickets[i].MessangInfo();
            }
            Console.WriteLine("\nИтоговая стоимость составила: {0}", Basket.Price);
            Console.WriteLine("---------------------------");
        }
        public static T ChooseEl<T>(List<T> elements)
        {
            ConsoleMessages.MessageToSelectItemEnterNumber();
            T el;
            do
            {
                string? inputNumber = Console.ReadLine();
                el = FindElByIndex(elements, inputNumber);
            } while (el == null || el.Equals(default(T)));
            return el;
        }
        public static T FindElByIndex<T>(List<T> list, string? indexStr)
        {
            int index;
            if (IsNumberInList(list, indexStr, out index))
            {
                return list[index - 1];
            }
            else
            {
                ConsoleMessages.MessageIncorrectInput();
                return default(T);
                // throw new ArgumentException("Выбранного елемента нет в списке");
            }
        }
        public static bool IsNumberInList<T>(List<T> films, string? indexStr, out int index)
        {
            bool tryParseChecked = int.TryParse(indexStr, out index);
            return tryParseChecked && films.Count >= index && index > 0;
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
        public static void OutputTimeFilmScreening(List<FilmScreening> filmscreenings) // добавить вывод свободных мест
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
        public static void MessageInfo(Film film) // Перенести в ConsoleMessages
        {
            Console.WriteLine("\nИнформация о фильме {0}", film.Name);
            Console.WriteLine("\nЖанр: {0}", film.Genre);
            Console.WriteLine("Год выхода: {0}", film.Year);
            Console.WriteLine("Описание: {0}\n", film.Description);
        }
    }
}
