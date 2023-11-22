namespace Model.Models
{
    public class CinemaChain
    {
        public CinemaChain(List<Cinema> cinemas)
        {
            Cinemas = cinemas;
        }
        public List<Cinema> Cinemas { get; }

        private int FindCinemaIndexById(int id, List<Cinema> cinemas)
        {
            int index = -1;
            for (int i = 0; i < cinemas.Count; i++)
            {
                if (cinemas[i].Id == id)
                {
                    return i;
                }
            }
            return index;
        }

        public void BookingPlaces(Basket basket)
        {
            for (int i = 0; i < basket.NumberTickets; i++)
            {
                int indexCinema = FindCinemaIndexById(basket.Tickets[i].IdCinema, Cinemas);
                Cinemas[indexCinema].BookingPlace(basket.Tickets[i].IdShow, basket.Tickets[i].Place);
            }
        }
    }
}
