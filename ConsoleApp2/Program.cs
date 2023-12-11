using CinemaModel;
using Data;
using MainFlow;
using View;

namespace ViewModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задачи:
            /*
                * Написать Unit Tests для новых методов в классе Seating
                * Разделить и переименовать ConsoleMessages
             */
            try
            {
                WorkingData data = new(args);
                CinemaChain cinemas = data.GetCinemaChain();

                Basket basket = ConsoleFlow.BuyTickets(cinemas.Cinemas);
                cinemas.BookingPlaces(basket);

                data.Save(cinemas);
                data.Save(basket);
            }
            catch (BootFileException ex)
            {
                ConsoleMessages.OutputDataErrorsText(ex.Message,ex.PathError);
            }
            catch(Exception ex)
            {
                ConsoleMessages.OutputErrorsText(ex.Message);
            }
            finally
            {
                ConsoleMessages.MessageCompletionProgram();
            }
        }
    }
}