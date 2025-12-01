using Gamza.Enums;

namespace Gamza.Models
{
    public partial class Player
    {
        public int Exp { get; set; } = 0;
        public Stauts PlayerStauts { get; set; } = Stauts.Str;
        public Stauts PlayerSecondStauts { get; set; } = Stauts.Dex;
        public long ExpToNext { get; set; } = 100;

        public int MaxHP { get; set; } = 100;
        public int HP { get; set; } = 100;
        public int MaxMP { get; set; } = 50;
        public int MP { get; set; } = 50;

        public int Defense { get; set; } = 0;
        public int PhysicalAttack { get; set; } = 10;
        public int MagicAttack { get; set; } = 0;

        public float AttackSpeed { get; set; } = 1.0f; // 초당 공격수
        public float MoveSpeed { get; set; } = 3.5f;
    }
}
