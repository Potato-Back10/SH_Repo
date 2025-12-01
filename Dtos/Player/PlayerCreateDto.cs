using Gamza.Enums;

namespace Gamza.Dtos
{
    public class PlayerCreateDto
    {
        public string NickName { get; set; } = string.Empty;
        public JobType? Job { get; set; }
        public Stauts PlayerStauts { get; set; } = Stauts.Str;
    }
}
