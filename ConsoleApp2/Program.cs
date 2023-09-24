using ConsoleApp2.Classes;
namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            List<Film> films = new List<Film>() {
            new Film(
                "Интерстеллар",
                "Научная фантастика",
                "Фильм рассказывает историю группы исследователей, которые отправляются в космическое путешествие, чтобы найти новый дом для человечества в другой галактике.",
                2014
                ),
            new Film(
                "Темный рыцарь",
                "Боевик",
                "Этот фильм о супергерое Бэтмене, который сражается с преступником по имени Джокер, чтобы спасти Готэм-сити.",
                2008
                ),
            new Film(
                "Зеленая миля",
                "Драма",
                "Фильм рассказывает историю тюремного смотрителя, который обнаруживает, что один из заключенных обладает необычными способностями.",
                1999
                ),
            new Film(
                "Властелин колец: Братство кольца",
                "Фэнтези",
                "Фильм о группе героев, отправляющихся в опасное путешествие, чтобы уничтожить кольцо власти.",
                2001
                )
            };
        }
       
        public static void OutputInfoFilm(Film films)
        {
            Console.WriteLine("Информация о фильме:");
            Console.WriteLine();
            Console.WriteLine("Название {0}",films.name);
            Console.WriteLine("Жанр {0}",films.genre);
            Console.WriteLine("Год выхода {0}",films.year);
            Console.WriteLine();
            Console.WriteLine("Для перехода назад напишите \"назад\"");
        }
        public static void OutputTimeFilmScreening(Film films)
        {
            Console.WriteLine("Время показа фильма:");
            Console.WriteLine();
            //дописать
        }

    }
}