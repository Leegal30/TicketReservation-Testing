using Microsoft.EntityFrameworkCore;
using TicketReservation.Model;

namespace TicketReservation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> events { get; set; }
        public DbSet<Booking> bookings { get; set; }
    }
}
