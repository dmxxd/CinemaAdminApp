using CinemaAdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaAdminApp.Logic
{
    public class TicketSaleManager
    {
        private List<Ticket> _tickets = new List<Ticket>();
        private List<FinancialOperation> _operations = new List<FinancialOperation>();
        private SessionManager _sessionManager = new SessionManager();
        private static int _nextTicketId = 1;
        private static int _nextOperationId = 1;

        public TicketSaleManager()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            if (!_tickets.Any())
            {
                _tickets.Add(new Ticket
                {
                    Id = _nextTicketId++,
                    SessionId = 1,
                    SeatNumber = 1,
                    Price = 350,
                    SaleTime = DateTime.Now.AddDays(-1)
                });

                _tickets.Add(new Ticket
                {
                    Id = _nextTicketId++,
                    SessionId = 1,
                    SeatNumber = 2,
                    Price = 350,
                    SaleTime = DateTime.Now.AddDays(-1)
                });

                _tickets.Add(new Ticket
                {
                    Id = _nextTicketId++,
                    SessionId = 2,
                    SeatNumber = 5,
                    Price = 300,
                    SaleTime = DateTime.Now
                });
            }

            if (!_operations.Any())
            {
                _operations.Add(new FinancialOperation
                {
                    Id = _nextOperationId++,
                    Type = OperationType.TicketSale,
                    Amount = 350,
                    DateTime = DateTime.Now.AddDays(-1),
                    Description = "Продажа билета на сеанс #1, место 1",
                    RelatedId = 1
                });

                _operations.Add(new FinancialOperation
                {
                    Id = _nextOperationId++,
                    Type = OperationType.TicketSale,
                    Amount = 350,
                    DateTime = DateTime.Now.AddDays(-1),
                    Description = "Продажа билета на сеанс #1, место 2",
                    RelatedId = 2
                });

                _operations.Add(new FinancialOperation
                {
                    Id = _nextOperationId++,
                    Type = OperationType.TicketSale,
                    Amount = 300,
                    DateTime = DateTime.Now,
                    Description = "Продажа билета на сеанс #2, место 5",
                    RelatedId = 3
                });
            }
        }

        public bool SellTicket(int sessionId, int seatNumber)
        {
            var session = _sessionManager.GetSessionById(sessionId);

            if (session == null) return false;

            if (_tickets.Any(t => t.SessionId == sessionId && t.SeatNumber == seatNumber))
            {
                return false;
            }

            var newTicket = new Ticket
            {
                Id = _nextTicketId++,
                SessionId = sessionId,
                SeatNumber = seatNumber,
                Price = session.Price,
                SaleTime = DateTime.Now
            };

            RegisterFinancialOperation(
                OperationType.TicketSale,
                newTicket.Price,
                $"Продажа билета на сеанс #{sessionId}, место {seatNumber}",
                newTicket.Id
            );

            _tickets.Add(newTicket);

            session.AvailableSeats--;

            return true;
        }

        public void RegisterFinancialOperation(OperationType type, decimal amount, string description, int? relatedId = null)
        {
            var newOp = new FinancialOperation
            {
                Id = _nextOperationId++,
                Type = type,
                Amount = amount,
                DateTime = DateTime.Now,
                Description = description,
                RelatedId = relatedId
            };
            _operations.Add(newOp);
        }

        public List<Ticket> GetAllTickets() => _tickets;

        public List<Ticket> GetTicketsForSession(int sessionId)
        {
            return _tickets.Where(t => t.SessionId == sessionId).ToList();
        }

        public List<FinancialOperation> GetAllOperations() => _operations;

        public decimal GetTotalRevenue()
        {
            return _operations.Where(o => o.Type == OperationType.TicketSale).Sum(o => o.Amount);
        }
    }
}
