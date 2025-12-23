using System.Collections.Generic;
using CinemaAdminApp.Models;
using System.Linq;

namespace CinemaAdminApp.Logic
{
    public class HallManager
    {
        private static List<Hall> _halls = new List<Hall>();

        public HallManager()
        {
            if (!_halls.Any())
            {
                _halls.Add(new Hall { Id = 1, Name = "Красный Зал", Capacity = 100 });
                _halls.Add(new Hall { Id = 2, Name = "Синий Зал", Capacity = 150 });
                _halls.Add(new Hall { Id = 3, Name = "VIP Зал", Capacity = 30 });
            }
        }

        public List<Hall> GetAllHalls()
        {
            return _halls;
        }

        public Hall GetHallById(int id)
        {
            return _halls.FirstOrDefault(h => h.Id == id);
        }
    }
}