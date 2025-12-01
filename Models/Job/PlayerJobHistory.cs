using System;

namespace Gamza.Models
{
    public class PlayerJobHistory
    {
        public long Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; } = default!;

        public int JobId { get; set; }
        public Job Job { get; set; } = default!;

        public DateTime ChangedAtUtc { get; set; }
    }
}
