using System;
using System.Collections.Generic;

// Модель данных для фильмов
class Movie
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Actors { get; set; }
    public List<string> Genres { get; set; }
    public Dictionary<DateTime, double> Showtimes { get; set; } // Дата и цена билета
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем несколько фильмов
        var movies = new List<Movie>
        {
            new Movie
            {
                Title = "Фильм 1",
                Description = "Описание фильма 1",
                Actors = new List<string> { "Актер 1", "Актер 2" },
                Genres = new List<string> { "Жанр 1", "Жанр 2" },
                Showtimes = new Dictionary<DateTime, double>
                {
                    { DateTime.Now.Date, 10.0 },
                    { DateTime.Now.Date.AddDays(1), 12.0 }
                }
            },
            new Movie
            {
                Title = "Фильм 2",
                Description = "Описание фильма 2",
                Actors = new List<string> { "Актер 3", "Актер 4" },
                Genres = new List<string> { "Жанр 3", "Жанр 4" },
                Showtimes = new Dictionary<DateTime, double>
                {
                    { DateTime.Now.Date, 8.0 },
                    { DateTime.Now.Date.AddDays(1), 9.0 }
                }
            }
        };

        Console.WriteLine("Список фильмов:");
        for (int i = 0; i < movies.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {movies[i].Title}");
        }

        Console.Write("Выберите номер фильма: ");
        int movieIndex = int.Parse(Console.ReadLine()) - 1;

        if (movieIndex >= 0 && movieIndex < movies.Count)
        {
            var selectedMovie = movies[movieIndex];
            ShowMovieDetails(selectedMovie);
        }
        else
        {
            Console.WriteLine("Некорректный выбор фильма.");
        }
    }

    static void ShowMovieDetails(Movie movie)
    {
        Console.WriteLine($"Название: {movie.Title}");
        Console.WriteLine($"Описание: {movie.Description}");
        Console.WriteLine($"Актеры: {string.Join(", ", movie.Actors)}");
        Console.WriteLine($"Жанры: {string.Join(", ", movie.Genres)}");

        Console.WriteLine("Доступные даты и цены:");
        foreach (var showtime in movie.Showtimes)
        {
            Console.WriteLine($"{showtime.Key.ToShortDateString()} - Цена: {showtime.Value} руб.");
        }

        Console.Write("Выберите дату (в формате dd.mm.yyyy): ");
        string selectedDateStr = Console.ReadLine();

        if (DateTime.TryParseExact(selectedDateStr, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
        {
            if (movie.Showtimes.ContainsKey(selectedDate))
            {
                double ticketPrice = movie.Showtimes[selectedDate];
                Console.WriteLine($"Вы выбрали {selectedDate.ToShortDateString()}. Цена билета: {ticketPrice} руб.");
                Console.Write("Введите количество билетов: ");
                int numberOfTickets = int.Parse(Console.ReadLine());

                double totalPrice = ticketPrice * numberOfTickets;
                Console.WriteLine($"Итого к оплате: {totalPrice} руб.");
            }
            else
            {
                Console.WriteLine("Для выбранной даты нет доступных сеансов.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный формат даты.");
        }
    }
}
