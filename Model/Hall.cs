using System;

namespace CinemaModel
{
    public class Hall
    {
        public Hall(int id, string screen, int[][] layout)
        {
            if (id < 0) throw new ArgumentException("id не может быть меньше нуля");
            else if (!CheckingDifferentCountRow(layout)) throw new ArgumentException("вместимость всех рядов должна быть одинакова");
            else if (!CheckingPositivNumberPlase(layout)) throw new ArgumentException("номер места не может быть меньше единицы");
            else if (!CheckingDyblicateNumberPlase(layout)) throw new ArgumentException("номера мест должны быть последовательными и возрастающими");
            else 
            {
                Id = id;
                Layout = layout;
                Screen = screen;
            }
        }
        public int Id { get; }
        public int[][] Layout { get; }
        public string Screen { get; } 
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
        private bool CheckingDifferentCountRow(int[][] layout)
        {
            for (int i = 1; i < layout.Length; i++)
            {
                if (layout[i-1].Length != layout[i].Length)
                    return false;
            }
            return true;
        }
        private bool CheckingPositivNumberPlase(int[][] layout)
        {
            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    if (layout[i][j] <= 0)
                        return false;
                }
            }
            return true;
        }
        private bool CheckingDyblicateNumberPlase(int[][] layout)
        {
            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 1; j < layout[i].Length; j++)
                {
                    if (layout[i][j] <= layout[i][j - 1])
                        return false;
                }
            }
            return true;
        }
    }
}
