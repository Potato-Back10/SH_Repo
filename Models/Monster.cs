using Gamza.Enums;
namespace Gamza.Models
{
    public class Monster
    {
        public MonsterType Type { get; set; }
        public int Exp { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
    }

}
