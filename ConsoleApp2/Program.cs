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
            try
            {
                WorkingData data = new(args);
                CinemaChain cinemas = data.GetCinemaChain();

                Basket basket = ConsoleFlow.BuyTickets(cinemas.Cinemas);
                cinemas.BookingPlaces(basket);

                data.Save(cinemas);
                data.Save(basket);
            }
            catch (BootDataException ex)
            {
                ConsoleMessages.OutputDataErrorsText(ex.Message,ex.PathError);
            }
            catch (ArgumentException ex)
            {
                ConsoleMessages.OutputErrorsText(ex.Message);
            }
            catch (Exception ex)
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