using System;
using System.Collections.Generic;

namespace Gamza.Models
{
    public class BattleSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsFinished { get; set; } = false;
        public DateTime StartBattle { get; set; } = DateTime.UtcNow;
        public DateTime? EndBattle { get; set; }

        public ICollection<BattleParticipant> Participants { get; set; } =
            new List<BattleParticipant>();
    }

    public enum ActorType
    {
        Player = 0,
        Monster = 1,
    }

    public enum TeamSide
    {
        Players = 0,
        Monsters = 1,
    }

    public class BattleParticipant
    {
        public long Id { get; set; }

        public Guid BattleId { get; set; }
        public BattleSession? Battle { get; set; } = null;

        public ActorType Type { get; set; }
        public int ActorId { get; set; }
        public TeamSide Team { get; set; } = TeamSide.Players;

        public string? Name { get; set; }
        public int Level { get; set; }

        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public float AttackSpeed { get; set; }
        public float MoveSpeed { get; set; }
        public double AttackCoolTime { get; set; } = 0.0;

        public bool IsAlive => HP > 0;
    }

    public class CombatLog
    {
        public long Id { get; set; }
        public Guid BattleId { get; set; }

        //시전자
        public long? SourceParticipantId { get; set; }

        //대상
        public long? TargetParticipantId { get; set; }

        public DateTime Ts { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } = string.Empty;

        public string? Payload { get; set; }
    }

    public class RewardGrant
    {
        public long Id { get; set; }
        public Guid BattleId { get; set; }
        public int PlayerId { get; set; }
        public DateTime Ts { get; set; } = DateTime.UtcNow;
    }
}
