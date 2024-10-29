﻿namespace TicketReservation.Model
{
    public class Event
    {
        public Guid EventId { get; set; }              
        public string EventName { get; set; }          
        public DateTime EventDate { get; set; }        
        public string Venue { get; set; }              
        public int Total_Seats { get; set; }           
        public int Available_Seats { get; set; }
    }
}
