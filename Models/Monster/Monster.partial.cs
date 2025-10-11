using Gamza.Enums;

namespace Gamza.Models
{
    public partial class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public MonsterType Type { get; set; }
    }
}
