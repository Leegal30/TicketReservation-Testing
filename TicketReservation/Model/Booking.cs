namespace TicketReservation.Model
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public string EventName { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
