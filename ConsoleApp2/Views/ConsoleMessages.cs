using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIS.Models;

namespace CIS.Views
{
    internal class ConsoleMessages
    {
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
    }
}
