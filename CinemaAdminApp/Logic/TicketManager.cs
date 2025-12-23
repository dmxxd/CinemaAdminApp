using System;
using System.Collections.Generic;
using System.Linq;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public class TicketManager
    {
        private static List<Ticket> _tickets = new List<Ticket>();
        private static int _nextId = 1;
        public List<Ticket> GetAllTicketsForSession(int sessionId)
        {
            return _tickets.Where(t => t.SessionId == sessionId).ToList();
        }
        public void DeleteTicketsBySession(int sessionId)
        {
            _tickets.RemoveAll(t => t.SessionId == sessionId);
        }

        public List<Ticket> GetSoldTicketsBySession(int sessionId)
        {
            return _tickets.Where(t => t.SessionId == sessionId && t.IsSold).ToList();
        }

        public List<int> GetAvailableSeats(int sessionId, int hallCapacity)
        {
            List<int> allSeats = Enumerable.Range(1, hallCapacity).ToList();
            List<int> soldSeats = GetSoldTicketsBySession(sessionId).Select(t => t.SeatNumber).ToList();

            return allSeats.Except(soldSeats).ToList();
        }

        public Ticket SellTicket(int sessionId, int seatNumber, decimal price)
        {
            Ticket newTicket = new Ticket
            {
                Id = _nextId++,
                SessionId = sessionId,
                SeatNumber = seatNumber,
                Price = price,
                SaleTime = DateTime.Now,
                IsSold = true
            };

            _tickets.Add(newTicket);
            return newTicket;
        }
    }
}