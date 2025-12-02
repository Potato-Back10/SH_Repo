namespace Gamza.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string LoginID { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "User";

        // 한 계정에 여러 캐릭터
        public List<Player> Players { get; set; } = new();
    }
}
