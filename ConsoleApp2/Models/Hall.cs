using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Models
{
    internal class Hall
    {
        public Hall(int number, string screen, int[][] layout) 
        {
            Layout = layout;
            Number = number;
            Screen = screen;
        }
        public int[][] Layout { get; }
        public int Number { get; }
        public string Screen { get; } // строка или отдельный класс?
        public int CountPlase { get { return CalcCoutPlase(Layout); } }
        public int CountPows { get { return Layout.GetLength(0); } }

        private int CalcCoutPlase(int[][] layout)
        {
            int count = 0;
            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    count++;
                }
            }
            return count;
        }

    }
}
