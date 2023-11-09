namespace Model.Models
{
    public class Basket 
    {
        public List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        public int NumberTickets { get { return Tickets.Count; }}
        public int Price { get; private set; } = 0;
        public void AddTicket(Ticket ticket)
        {
                Tickets.Add(ticket);
                Price += ticket.Place.Price;
        }
    }
}
