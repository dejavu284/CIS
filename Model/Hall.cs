using System;

namespace CinemaModel
{
    public class Hall
    {
        public Hall(int id, string screen, int[][] layout)
        {
            Id = id;
            Layout = layout;
            Screen = screen;
        }
        public int Id { get; }
        public int[][] Layout { get; }
        public string Screen { get; } // строка или отдельный класс?
        public int CountPlase { get { return CalcCoutPlase(Layout); } }
        public int CountRows { get { return Layout.GetLength(0); } }
        public int CountCols { get { return CountPlase / CountRows; } }

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
