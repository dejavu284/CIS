namespace CinemaModel
{
    public class CinemaChain
    {
        public CinemaChain(List<Cinema> cinemas)
        {
            Cinemas = cinemas;
        }
        public List<Cinema> Cinemas { get; }

        private int FindCinemaIndexById(int id)
        {
            int index = -1;
            for (int i = 0; i < Cinemas.Count; i++)
            {
                if (Cinemas[i].Id == id)
                {
                    return i;
                }
            }
            return index;
        }
        /*public Cinema? FindCinemaById(int id)
        {
            
            for (int i = 0; i < Cinemas.Count; i++)
            {
                if (Cinemas[i].Id == id)
                {
                    return Cinemas[i];
                }
            }
            return null;
        }
*/
        public void BookingPlaces(Basket basket)
        {
            for (int i = 0; i < basket.NumberTickets; i++)
            {
                int indexCinema = FindCinemaIndexById(basket.Tickets[i].IdCinema);
                Cinemas[indexCinema].BookingPlace(basket.Tickets[i].IdShow, basket.Tickets[i].Place);
            }
        }
    }
}
