using Gamza.Enums;

namespace Gamza.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string NickName { get; set; } = "";
        public int Level { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public Stauts PlayerStauts { get; set; } = Stauts.Str;
        public JobType? Job { get; set; }

        public int CurrentJobId { get; set; }
        public Job CurrentJob { get; set; } = default!;

        // 히스토리
        public ICollection<PlayerJobHistory> JobHistories { get; set; } =
            new List<PlayerJobHistory>();
    }
}
