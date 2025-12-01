using Gamza.Enums;

namespace Gamza.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public string NickName { get; set; } = "";
        public int Level { get; set; } = 1;
        public JobType? Job { get; set; }

        // 계정과 연결 (FK)
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
