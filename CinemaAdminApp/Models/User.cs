namespace CinemaAdminApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; }
        public string FullName => Username;
    }
}
