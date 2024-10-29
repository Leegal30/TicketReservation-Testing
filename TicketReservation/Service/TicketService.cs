using Microsoft.EntityFrameworkCore;
using System.Collections;
using TicketReservation.Data;
using TicketReservation.Model;

namespace TicketReservation.Repository
{
    public class TicketService(ApplicationDbContext context) 
    {
        public async Task<bool> CreateAsync(EventDto eventDto)
        {
            var Nevent = new Event()
            {
                EventName = eventDto.EventName,
                EventDate = eventDto.EventDate,
                Venue = eventDto.Venue,
                Total_Seats = eventDto.Total_Seats,
                Available_Seats = eventDto.Available_Seats
            };
            context!.events.Add(Nevent);
            var result = await context.SaveChangesAsync();
            Console.WriteLine(result);
            return result > 0;
        }

        public async Task<Booking> BookAsync(BookingDto bookingreq)
        {
            var eventObj = await context.events.FirstOrDefaultAsync(e => e.EventId == bookingreq.EventId);
            var booking = new Booking()
            {
                BookingId = Guid.NewGuid(),
                EventId = bookingreq.EventId,
                EventName = eventObj.EventName,
                NumberOfTickets = bookingreq.NumberOfTickets,
                UserId = Guid.NewGuid()
            };

            eventObj.Available_Seats -= bookingreq.NumberOfTickets;
            context!.bookings.Add(booking);
            await context.SaveChangesAsync();
            return booking;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var getEvent = await context.bookings.FirstOrDefaultAsync(x => x.BookingId == id);
            if (getEvent != null)
            {
                context.bookings.Remove(getEvent);
                var result = await context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<IEnumerable> GetBookingsAsync()
        {
            var bookings = await context.bookings.ToListAsync();
            return bookings;
            
        }

        public async Task<IEnumerable> GetEventsAsync()
        {
            var events = await context.events.ToListAsync();
            return events;
        }
    }
}
