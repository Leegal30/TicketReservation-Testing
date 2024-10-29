using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketReservation.Data;
using TicketReservation.Model;
using TicketReservation.Repository;

namespace TicketReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(TicketService TService) : ControllerBase
    {
        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await TService.GetEventsAsync();
            if (events == null)
            {
                return NotFound();
            }
            return Ok(events);
        }

        [HttpGet("Bookings")]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await TService.GetBookingsAsync();
            if (bookings == null)
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        [HttpPost("book-ticket")]
        public async Task<IActionResult> BookTicket(BookingDto bookingDto)
        {
            var booking = await TService.BookAsync(bookingDto);
            if(booking == null)
            {
                return NotFound();
            }
            return Ok(booking); 
        }

        [HttpPost("post-event")]
         public async Task<IActionResult> Create(EventDto eventDto)
         {
            var Nevent =await TService.CreateAsync(eventDto);
            if (Nevent)
            {
                return CreatedAtAction(nameof(Create), eventDto);
            }
            return BadRequest();
         }

        [HttpDelete("CancelBooking/{bookingId}")]
        public async Task<IActionResult> Delete(Guid bookingId)
        {
            var status = await TService.DeleteAsync(bookingId);
            if(status == null)
            {
                return BadRequest();
            }
            return Ok(status);
        }
    }
}

