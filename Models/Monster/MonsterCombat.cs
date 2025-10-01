using Gamza.Enums;

namespace Gamza.Models
{
    public partial class Monster
    {
        public int Level { get; set; } = 1;

        public int MaxHP { get; set; } = 30;
        public int HP { get; set; } = 30;
        public int Attack { get; set; } = 5;
        public int Defense { get; set; } = 0;
        //처치시 경험치량
        public int RewardExp { get; set; } = 10;
    }
}
