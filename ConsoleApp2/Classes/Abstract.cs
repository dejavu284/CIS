using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Classes
{
    internal class Abstract
    {
        public static T ChooseEl<T>(List<T> elements)//повтор методов вопрос что делать
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
    }
}
