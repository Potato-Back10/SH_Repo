using System.Collections.Generic;
using Gamza.Enums;

namespace Gamza.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public JobTier Tier { get; set; } = default!;
        public int MinLevel { get; set; }

        public int? ParentId { get; set; }
        public Job? Parent { get; set; }
        public ICollection<Job> Children { get; set; } = new List<Job>();
    }
}
