using Gamza.Enums;

namespace Gamza.Dtos
{
    public class PlayerResponseDto
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Exp { get; set; }
        public JobType? Job { get; set; }
        public Stauts PlayerStauts { get; set; }
    }
}
