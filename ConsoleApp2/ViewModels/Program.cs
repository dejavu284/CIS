
using CIS.Models;
using CIS.Views;
using CIS.Data;


namespace CIS.ViewModels
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (WorkingData.DataIsCorrect(args))
            {
                WorkingData data = new(args);
                List<Cinema> cinemas = new();

                if (data.TryDeserializ(data.CinemasJsonPath, ref cinemas))
                {
                    Basket basket = MainViewModel.BuyTickets(cinemas);
                    WorkingData.Save(data.BasketJsonPath, basket);
                    ConsoleMessages.MessageCompletionProgram();
                }
                else
                    ConsoleMessages.MessageIncorrectInput();
            }
            else
            {
                ConsoleMessages.MessageIncorrectInput();
            }
        }
    }
}