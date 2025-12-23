namespace CinemaAdminApp.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
    }
}