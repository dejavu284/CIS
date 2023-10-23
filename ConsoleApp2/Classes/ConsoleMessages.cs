using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
