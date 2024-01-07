namespace CinemaModel
{
    public class Basket
    {
        public List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        public int NumberTickets { get { return Tickets.Count; } }
        public int Price 
        { 
            get
            {
                int price = 0;
                foreach (Ticket ticket in Tickets)
                {
                    price += ticket.Place.Price;
                }
                return price;
            }
        }
        public void AddTicket(Ticket ticket)
        {
            if (Tickets.IndexOf(ticket) == -1)
            {
                Tickets.Add(ticket);
            }
            else throw new Exception($"Билет с местом на {ticket.Place.Row +1}-ом ряду с номером {ticket.Place.Number}, уже есть в корзине");
        }
        public void RemoveTicket(Ticket ticket) 
        { 
            Tickets.Remove(ticket);
        }
        public void Clear()
        {
            Tickets.Clear();
        }
    }
}
