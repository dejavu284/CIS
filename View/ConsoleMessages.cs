using CinemaModel;
using System;
using System.Reflection;

namespace View
{
    public class ConsoleMessages
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
        public static void OutputErrorsText(string errorText)
        {
            Console.WriteLine(errorText);
        }
        public static void OutputDataErrorsText(string errorText,string errorPath)
        {
            Console.WriteLine("{0}.\n\n В при работе с файлом по пути: {1}",errorText,errorPath);
        }
        public static void MessageBacketInfo(Basket basket)
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Чек:");
            for (int i = 0; i < basket.NumberTickets; i++)
            {
                Console.WriteLine("\nБилет №{0}", i + 1);
                MessangTicketInfo(basket.Tickets[i]);
            }
            Console.WriteLine("\nИтоговая стоимость составила: {0}", basket.Price);
            Console.WriteLine("---------------------------");
        }
        public static void MessangTicketInfo(Ticket ticket)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Информация о билете:");
            Console.WriteLine("Название фильма: {0}", ticket.NameFilm);
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
        public static int ChooseIndexRow(Show showInCertainTime)
        {
            MessageToSelectItemEnterNumber("ряда");
            do
            {
                int numberRow = EnteringNumber();
                if (!showInCertainTime.Seating.CheckExistenceRow(numberRow))
                {
                    MessageIncorrectInput();   
                }
                else if(!showInCertainTime.Seating.CheckAvailabilityRow(numberRow))
                {
                    MessageRowOccupied();
                }
                else
                {
                    return numberRow - 1;
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
        public static int ChooseIndexColum(Show showInCertainTime, int indexRow)
        {
            MessageToSelectItemEnterNumber("места");
            do
            {
                int numberColum = EnteringNumber();
                if (numberColum >= showInCertainTime.Seating.Places[indexRow].Length || numberColum < 1)
                {
                    MessageIncorrectInput();
                }
                else if (!showInCertainTime.Seating.CheckAvailabilityPlace(indexRow + 1,numberColum))
                {
                    MessagePlaceOccupied();
                }
                else
                {
                    return numberColum-1;
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
                int numberEl = EnteringNumber();
                if(numberEl <= elements.Count && numberEl > 0)
                {
                    return elements[numberEl - 1];
                }
                else
                {
                    MessageIncorrectInput(); // throw new ArgumentException("Выбранного елемента нет в списке");
                }
            } while (true);
        }
        public static int EnteringNumber()
        {
            int number;
            string? inputNumber;
            do
            {
                inputNumber = Console.ReadLine();
                if (int.TryParse(inputNumber, out number))
                    break;
                else
                    MessageIncorrectInput();
                
            } while (true);
            return number;
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
                Console.WriteLine("\n{0}. {1}. Количество мест: {2}", i + 1, shows[i].Time, shows[i].Seating.CountAvailableSeats);
            }
        }
        public static void OutputCountPlace(Show show)
        {
            Console.WriteLine("Количесво оставшихся мест на сеанс: {0}\n", show.Seating.CountAvailableSeats);
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
        public static void MessageFilmInfo(Film film)
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
            Console.WriteLine("\nАдрес: {0} {1} ",cinema.Address.Street,cinema.Address.NumberHouse);
            Console.WriteLine("Рейтинг: {0}\n",Math.Round(cinema.Rating,1));
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
