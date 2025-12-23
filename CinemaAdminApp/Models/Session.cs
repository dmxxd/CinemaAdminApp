namespace CinemaAdminApp.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public string FilmTitle { get; set; }
        public int HallId { get; set; }
        public string HallName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public string StartTime => $"{Date} {Time}";

        public string DisplayInfo => $"{FilmTitle} | {Date} {Time} | {Price} ₽ | Свободно: {AvailableSeats} мест";
    }
}