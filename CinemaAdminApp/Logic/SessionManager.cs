using System.Collections.Generic;
using System.Linq;
using CinemaAdminApp.Models;

namespace CinemaAdminApp.Logic
{
    public class SessionManager
    {
        public List<Session> GetAllSessions() => DataStorage.GetAllSessions();

        public List<Film> GetAllFilms() => DataStorage.GetAllFilms();

        public List<Hall> GetAllHalls() => DataStorage.GetAllHalls();

        public Session GetSessionById(int id) => DataStorage.GetSessionById(id);

        public bool CreateSession(Session session) => DataStorage.AddSession(session);

        public bool UpdateSession(Session session) => DataStorage.UpdateSession(session);

        public bool DeleteSession(int id) => DataStorage.DeleteSession(id);

        public bool SellTicket(int sessionId, int seatNumber)
        {
            var session = DataStorage.GetSessionById(sessionId);
            if (session == null) return false;

            return DataStorage.SellTicket(sessionId, seatNumber, session.Price);
        }

        public List<int> GetSoldSeatsForSession(int sessionId) => DataStorage.GetSoldSeatsForSession(sessionId);

        public List<Session> GetAvailableSessions() => DataStorage.GetAllSessions().Where(s => s.AvailableSeats > 0).ToList();

        public List<Session> GetSessionsByFilm(int filmId) => DataStorage.GetAllSessions().Where(s => s.FilmId == filmId).ToList();

        public List<Session> GetSessionsByDate(string date) => DataStorage.GetAllSessions().Where(s => s.Date == date).ToList();
    }
}