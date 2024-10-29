using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReservation.Controllers;
using TicketReservation.Data;
using TicketReservation.Repository;
using TicketReservation.Model;

namespace TicketReservation.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        private ApplicationDbContext _context;
        private TicketService _repository;
        private TicketController _controller;

        [SetUp]
        public void Setup()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TicketReservationDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new TicketService(_context);
            _controller = new TicketController(_repository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetEvents_ReturnsOkResult_WithEvents()
        {
            
            var event1 = new Event
            {
                EventId = Guid.NewGuid(),
                EventName = "Concert",
                EventDate = DateTime.UtcNow,
                Venue = "Chennai",
                Total_Seats = 100000,
                Available_Seats = 100000
            };
            var event2 = new Event
            {
                EventId = Guid.NewGuid(),
                EventName = "Conference",
                EventDate = DateTime.UtcNow,
                Venue = "Chennai",
                Total_Seats = 100000,
                Available_Seats = 100000
            };
            await _context.events.AddRangeAsync(event1, event2);
            await _context.SaveChangesAsync();

            
            var result = await _controller.GetEvents();

            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var events = okResult.Value as IEnumerable<Event>;
            Assert.AreEqual(2, events.Count());
        }

        [Test]
        public async Task BookTicket_ReturnsOkResult_WithBooking()
        {
            // Arrange
            var eventObj = new Event
            {
                EventId = Guid.NewGuid(),
                EventName = "Concert",
                Venue = "Venue A",
                Total_Seats = 100,
                Available_Seats = 100
            };
            await _context.events.AddAsync(eventObj);
            await _context.SaveChangesAsync();

            var bookingDto = new BookingDto
            {
                EventId = eventObj.EventId,
                NumberOfTickets = 2
            };

            var result = await _controller.BookTicket(bookingDto);

            
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var booking = okResult.Value as Booking;
            Assert.AreEqual(2, booking.NumberOfTickets);
            Assert.AreEqual(98, eventObj.Available_Seats);
        }

        [Test]
        public async Task Create_ReturnsCreatedResult_WhenEventIsCreated()
        {
            
            var eventDto = new EventDto { EventName = "Concert", Venue = "Venue A", EventDate = DateTime.UtcNow };

            
            var result = await _controller.Create(eventDto);

            
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task Delete_ReturnsOkObjectResult_WhenDeletionSucceeds()
        {
            
            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                EventId = Guid.NewGuid(),
                EventName = "Concert", 
                NumberOfTickets = 2,
                UserId = Guid.NewGuid()
            };
            await _context.bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            
            var result = await _controller.Delete(booking.BookingId);

            // Expecting OkObjectResult
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
        }
    }
}
