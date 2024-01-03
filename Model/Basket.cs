namespace CinemaModel
{
    public class Basket
    {
        public List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        public int NumberTickets { get { return Tickets.Count; } }
        public int Price { get; private set; } = 0;
        public void AddTicket(Ticket ticket)
        {
            if (Tickets.IndexOf(ticket) == -1)
            {
                Tickets.Add(ticket);
                Price += ticket.Place.Price;
            }
            else throw new Exception($"Билет с местом на {ticket.Place.Row}ом ряду с номером {ticket.Place.Colum}, уже есть в корзине");
        }
    }
}
