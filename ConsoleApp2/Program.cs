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
                int? x = null;
                data.Save(x);

                Basket basket = Flow.BuyTickets(cinemas.Cinemas);
                cinemas.BookingPlaces(basket);

                data.Save(cinemas);
                data.Save(basket);
            }
            catch (DataException ex)
            {
                ConsoleMessages.OutputDataErrorsText(ex.Message,ex.PathError);
            }
            catch(Exception ex)
            {
                ConsoleMessages.OutputErrorsText(ex.Message);
            }
        }
    }
}