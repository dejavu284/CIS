using CIS.Models;

namespace CIS.Views
{
    internal class ConsoleMessages
    {
        public static bool PoolYesOrNo(string question)
        {
            string yes = "y";
            string no = "n";
            Console.WriteLine("\n{0}? ({1}/{2})\n", question, yes, no);
            while (true)
            {
                string answer = Console.ReadLine()!;
                if (answer!.ToLower() == yes)
                    return true;
                else if (answer.ToLower() == no)
                    return false;
                else
                    MessageIncorrectInput();
            }
        }
        public static void MessageIncorrectInput()
        {
            Console.WriteLine("\nНекорректный ввод поробуйте ещё раз\n");
        }
        public static void MessageCheck(Basket basket)
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Чек:");
            for (int i = 0; i < basket.NumberTickets; i++)
            {
                Console.WriteLine("\nБилет №{0}", i + 1);
                MessangInfo(basket.Tickets[i]);
            }
            Console.WriteLine("\nИтоговая стоимость составила: {0}", basket.Price);
            Console.WriteLine("---------------------------");
        }
        public static void MessangInfo(Ticket ticket)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Информация о билете:");
            Console.WriteLine("Название фильма: {0}", ticket.Name);
            Console.WriteLine("Дата сеанса: {0}", ticket.Date);
            Console.WriteLine("Время сеанса: {0}", ticket.Time);
            Console.WriteLine("Ряд {0}, место {1}", ticket.Place.Row + 1, ticket.Place.Colum + 1);
            Console.WriteLine("Цена билета: {0}", ticket.Place.Price);
            Console.WriteLine("---------------------------\n");
        }
        public static void MessageBookingRequeast()
        {
            Console.WriteLine("Укажите ряд и место, которое хотите забронировать.");
        }
        public static void MessagePlaceCost(Show shoInCertainTime, int row, int col)
        {
            Console.WriteLine("Стоимость покупки места состовляет: {0}", shoInCertainTime.Seating.Places[row][col]);
        }
        public static int ChooseRow(Show showInCertainTime)
        {
            do
            {
                Console.WriteLine("Ряд:");
                string strRow = Console.ReadLine();
                int row;
                if (IsNumberInList(showInCertainTime.Seating.Places.GetLength(0), strRow, out row))
                {
                    for (int j = 0; j < showInCertainTime.Seating.Places[row-1].Length; j++) // перенести в seating.cs
                    {
                        if(showInCertainTime.Seating.Places[row - 1][j] != -1)
                        {
                            return row - 1;
                        }
                    }
                    MessageRowOccupied();
                    continue;
                }
                else
                {
                    MessageIncorrectInput();
                }
            } while (true);
        }
        public static void MessagePlaceOccupied()
        {
            Console.WriteLine("Выбранное место занято, пожалуйста, выберете другое.");
        }
        public static void MessageRowOccupied()
        {
            Console.WriteLine("Все места в ряду заняты, пожалуйста, выберете другой.");
        }
        public static int ChooseCol(Show showInCertainTime, int row)
        {
            do
            {
                Console.WriteLine("Место:");
                string strCol = Console.ReadLine()!;
                int col;
                if (IsNumberInList(showInCertainTime.Seating.Places[row].Length, strCol, out col))
                {
                    if (showInCertainTime.Seating.Places[row][col-1] != -1) // перенести в seating.cs
                    {
                        return col - 1;
                    }
                    else
                    {
                        MessagePlaceOccupied();
                        continue;
                    }
                }
                else
                {
                    MessageIncorrectInput();
                }
            } while (true);
        }
        public static void MessageToSelectItemEnterNumber(string nameOfTypeElement)
        {
            Console.WriteLine("\nДля выбора {0} введите его номер", nameOfTypeElement);
            Console.WriteLine();
        }
        public static T ChooseEl<T>(List<T> elements, string nameOfTypeElement)
        {
            MessageToSelectItemEnterNumber(nameOfTypeElement);
            do
            {
                string? inputNumber = Console.ReadLine();
                Console.WriteLine();
                int index;
                if(IsNumberInList(elements.Count, inputNumber, out index))
                {
                    return elements[index - 1];
                }
                else
                {
                    MessageIncorrectInput(); // throw new ArgumentException("Выбранного елемента нет в списке");
                }
            } while (true);
        }
        private static bool IsNumberInList(int count, string? indexStr, out int index)
        {
            bool tryParseChecked = int.TryParse(indexStr, out index);
            return tryParseChecked && count >= index && index > 0;
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
        public static void OutputTimeShow(List<Show> shows)
        {
            Console.WriteLine("Время показа фильма:");
            for (int i = 0; i < shows.Count; i++)
            {
                Console.WriteLine("\n{0}. {1}. Количество мест: {2}", i + 1, shows[i].Time, shows[i].Seating.CountAvailablePlaces);
            }
        }
        public static void OutputCountPlace(Show show)
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", show.Seating.CountAvailablePlaces);
        }
        public static void OutputDateShow(List<DateOnly> datesShows)
        {
            Console.WriteLine("Даты показа фильма:\n");
            for (int i = 0; i < datesShows.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, datesShows[i]);
            }
            Console.WriteLine();
        }
        public static void MessagePlaceNotExist()
        {
            Console.WriteLine("На выбранное время мест нет.\n");
        }
        public static void MessageInfo(Film film)
        {
            Console.WriteLine("\nИнформация о фильме {0}", film.Name);
            Console.WriteLine("\nЖанр: {0}", film.Genre);
            Console.WriteLine("Год выхода: {0}", film.Year);
            Console.WriteLine("Описание: {0}\n", film.Description);
        }
        public static void MessageNamesAllFilms(Poster films)
        {
            Console.WriteLine("\nФильмы в прокате:\n");
            for (int i = 0; i < films.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, films.Films[i].Name);
            }
        }
        public static void OutputCinemas(List<Cinema> cinemas)
        {
            Console.WriteLine("Кинотеатры:\n");
            for (int i = 0; i < cinemas.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, cinemas[i].Name);
            }
        }
        public static void MessageInfoCinema(Cinema cinema)
        {
            Console.WriteLine("\nИнформация о кинотеатре \"{0}\": ",cinema.Name);
            Console.WriteLine("\nАдрес: {0} ",cinema.Address);
            Console.WriteLine("Рейтинг:(добавить)\n");
        }
        public static void OutputSeatings(int[][] places)
        {
            Console.WriteLine("Схема зала указана ниже:\n");
            Console.WriteLine("Места, отмеченные знаком '*' - заняты.\n");
            for (int i = 0; i < places.GetLength(0); i++)
            {
                Console.Write("Ряд {0}. Места: ", i + 1);
                for (int j = 0; j < places[0].Length; j++)
                {
                    if (places[i][j] != -1)
                    {
                        Console.Write("{0}  ", j + 1);
                    }
                    else
                    {
                        Console.Write("{0}* ", j + 1);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
