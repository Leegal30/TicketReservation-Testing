namespace TicketReservation.Model
{
    public class BookingDto
    {
        public int NumberOfTickets { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; }
    }
}
