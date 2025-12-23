namespace CinemaAdminApp.Models
{
    public enum UserRole { Operator, Administrator }

    public class Personal
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public double HourlyRate { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}