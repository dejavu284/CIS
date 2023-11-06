

namespace CIS.Models
{
    internal class Basket 
    {
        public List<Ticket> Tickets { get; private set; } = new List<Ticket>();
        public int NumberTickets { get; private set; } = 0;
        public int Price { get; private set; } = 0;
        public void AddTicket(Show show)
        {
            Tickets.Add(new Ticket(show.Name, show.Date, show.Time, show.Price));
            Price += show.Price;
            NumberTickets++;
        }
    }
}
