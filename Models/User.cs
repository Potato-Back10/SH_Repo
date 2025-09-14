namespace Gamza.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string UserID { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Job { get; set; } = "";
    }
}
