using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Models;

namespace ConsoleApp2.Classes
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
    }
}
