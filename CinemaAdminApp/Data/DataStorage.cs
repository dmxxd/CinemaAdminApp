using System;
using System.Collections.Generic;
using System.Linq;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public static class DataStorage
    {
        private static List<Film> _films = new List<Film>();
        private static List<Hall> _halls = new List<Hall>();
        private static List<Session> _sessions = new List<Session>();
        private static List<Ticket> _tickets = new List<Ticket>();
        private static List<Personal> _personnel = new List<Personal>();
        private static List<InventoryItem> _inventory = new List<InventoryItem>();

        private static int _nextFilmId = 1;
        private static int _nextHallId = 1;
        private static int _nextSessionId = 1;
        private static int _nextTicketId = 1;

        static DataStorage()
        {
            InitializeSampleData();
        }

        private static void InitializeSampleData()
        {
            _halls.Add(new Hall { Id = _nextHallId++, Name = "Зал №1", Capacity = 100 });
            _halls.Add(new Hall { Id = _nextHallId++, Name = "Зал №2", Capacity = 80 });
            _halls.Add(new Hall { Id = _nextHallId++, Name = "VIP Зал", Capacity = 50 });
        }
        public static List<Ticket> GetAllTickets() => _tickets.OrderBy(t => t.SaleTime).ToList();

        public static List<Ticket> GetTicketsForSession(int sessionId) =>
            _tickets.Where(t => t.SessionId == sessionId).ToList();

        public static List<int> GetSoldSeatsForSession(int sessionId) =>
            _tickets
                .Where(t => t.SessionId == sessionId && t.IsSold)
                .Select(t => t.SeatNumber)
                .ToList();

        public static bool SellTicket(int sessionId, int seatNumber, decimal price)
        {
            if (_tickets.Any(t => t.SessionId == sessionId && t.SeatNumber == seatNumber && t.IsSold))
                return false;

            var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null || session.AvailableSeats <= 0)
                return false;

            var ticket = new Ticket
            {
                Id = _nextTicketId++,
                SessionId = sessionId,
                SeatNumber = seatNumber,
                Price = price,
                SaleTime = DateTime.Now,
                IsSold = true
            };

            _tickets.Add(ticket);
            session.AvailableSeats--;

            return true;
        }

        public static bool CancelTicket(int ticketId)
        {
            var ticket = _tickets.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null || !ticket.IsSold) return false;

            var session = _sessions.FirstOrDefault(s => s.Id == ticket.SessionId);
            if (session != null)
            {
                session.AvailableSeats++;
            }

            ticket.IsSold = false;
            return true;
        }
        public static List<Film> GetAllFilms() => _films.OrderBy(f => f.Title).ToList();

        public static Film GetFilmById(int id) => _films.FirstOrDefault(f => f.Id == id);

        public static bool AddFilm(Film film)
        {
            if (film == null || string.IsNullOrWhiteSpace(film.Title))
                return false;

            if (_films.Any(f => f.Title.Equals(film.Title, StringComparison.OrdinalIgnoreCase)))
                return false;

            film.Id = _nextFilmId++;
            _films.Add(film);
            return true;
        }

        public static bool UpdateFilm(Film film)
        {
            var existing = _films.FirstOrDefault(f => f.Id == film.Id);
            if (existing == null) return false;

            if (_films.Any(f => f.Id != film.Id && f.Title.Equals(film.Title, StringComparison.OrdinalIgnoreCase)))
                return false;

            existing.Title = film.Title;
            existing.Genre = film.Genre;
            existing.Duration = film.Duration;
            existing.Description = film.Description;
            existing.Rating = film.Rating;
            return true;
        }

        public static bool DeleteFilm(int id)
        {
            var film = _films.FirstOrDefault(f => f.Id == id);
            if (film == null) return false;

            return _films.Remove(film);
        }

        public static List<Hall> GetAllHalls() => _halls.OrderBy(h => h.Name).ToList();

        public static Hall GetHallById(int id) => _halls.FirstOrDefault(h => h.Id == id);

        public static bool AddHall(Hall hall)
        {
            if (hall == null || string.IsNullOrWhiteSpace(hall.Name))
                return false;

            if (_halls.Any(h => h.Name.Equals(hall.Name, StringComparison.OrdinalIgnoreCase)))
                return false;

            hall.Id = _nextHallId++;
            _halls.Add(hall);
            return true;
        }

        public static List<Session> GetAllSessions() => _sessions.OrderBy(s => s.Date).ThenBy(s => s.Time).ToList();

        public static Session GetSessionById(int id) => _sessions.FirstOrDefault(s => s.Id == id);

        public static bool AddSession(Session session)
        {
            if (session == null) return false;

            session.Id = _nextSessionId++;
            _sessions.Add(session);
            return true;
        }

        public static bool UpdateSession(Session session)
        {
            var existing = _sessions.FirstOrDefault(s => s.Id == session.Id);
            if (existing == null) return false;

            existing.FilmId = session.FilmId;
            existing.FilmTitle = session.FilmTitle;
            existing.HallId = session.HallId;
            existing.HallName = session.HallName;
            existing.Date = session.Date;
            existing.Time = session.Time;
            existing.Price = session.Price;
            existing.AvailableSeats = session.AvailableSeats;
            return true;
        }

        public static bool DeleteSession(int id)
        {
            var session = _sessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return false;

            _tickets.RemoveAll(t => t.SessionId == id);

            return _sessions.Remove(session);
        }
    }
}