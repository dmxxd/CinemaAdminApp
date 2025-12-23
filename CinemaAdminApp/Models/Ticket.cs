using System;

namespace CinemaAdminApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SeatNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime SaleTime { get; set; }
        public bool IsSold { get; set; }
    }
}
