using Gamza.Data;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamze.Services;

public class BattleService
{
    private readonly AppDbContext _db;
    public BattleService(AppDbContext db) => _db = db;

    //전투시작
    public async Task<BattleSession> StartBattle(int playerId, int[] monsterId, CancellationToken ct)
    {
        var player = await _db.Players.FirstAsync(p => p.Id == playerId, ct);
        var monsters = await _db.Monsters.Where(m => monsterId.Contains(m.Id)).ToListAsync(ct);

        var session = new BattleSession();
        session.Participants.Add(new BattleParticipant
        {
            Type = ActorType.Player,
            ActorId = player.Id,
            Team = TeamSide.Players,
            Name = player.NickName,
            Level = player.Level,
            MaxHP = player.MaxHP,
            HP = player.HP,
            Attack = player.PhysicalAttack,
            Defense = player.Defense,
            AttackSpeed = player.AttackSpeed,
            MoveSpeed = player.MoveSpeed,
            AttackCoolTime = 0
        });

        foreach (var m in monsters)
        {
            session.Participants.Add(new BattleParticipant
            {
                Type = ActorType.Monster,
                ActorId = m.Id,
                Team = TeamSide.Monsters,
                Name = m.Name,
                Level = m.Level,
                MaxHP = m.MaxHP,
                HP = m.MaxHP,
                Attack = m.Attack,
                Defense = m.Defense,
                AttackSpeed = 0.8f,
                MoveSpeed = 0f,
                AttackCoolTime = 0
            });
        }
        _db.Battles.Add(session);
        await _db.SaveChangesAsync(ct);
        return session;
    }

    public async Task<(bool finished, List<BattleParticipant> snap)> TickAsync(Guid battleId, int elapsedMs, CancellationToken ct = default)
    {
        var s = await _db.Battles
            .Include(x => x.Participants)
            .FirstAsync(x => x.Id == battleId, ct);

        if (s.IsFinished) return (true, s.Participants.ToList());

        foreach (var a in s.Participants)
            a.AttackCoolTime = Math.Max(0, a.AttackCoolTime - elapsedMs);

        BattleParticipant? SelectTarget(BattleParticipant actor)
        {
            if (!actor.IsAlive) return null;
            if (actor.Team == TeamSide.Players)
                return s.Participants.Where(p => p.Team == TeamSide.Monsters && p.IsAlive).OrderBy(p => p.HP).FirstOrDefault();
            else
                return s.Participants.Where(p => p.Team == TeamSide.Players && p.IsAlive).OrderBy(p => p.HP).FirstOrDefault();
        }

        foreach (var a in s.Participants.Where(p => p.IsAlive && p.AttackCoolTime <= 0))
        {
            var t = SelectTarget(a);
            if (t is null) continue;

            int effDef = Math.Max(0, t.Defense);
            int dmg = Math.Max(1, a.Attack - effDef);
            t.HP = Math.Max(0, t.HP - dmg);

            var aps = Math.Max(0.1f, a.AttackCoolTime);
            a.AttackCoolTime += 1000.0 / aps;

            _db.CombatLogs.Add(new CombatLog
            {
                BattleId = s.Id,
                SourceParticipantId = a.Id,
                TargetParticipantId = t.Id,
                //수정
                // Type - "Hit",
                // Payload = $"{{\"dmg\":{dmg}}}"
            });
        }

        bool playerAlive = s.Participants.Any(p => p.Team == TeamSide.Players && p.IsAlive);
        bool monsterAlive = s.Participants.Any(p => p.Team == TeamSide.Monsters && p.IsAlive);

        if (!playerAlive || !monsterAlive)
        {
            s.IsFinished = true;
            s.EndBattle = DateTime.UtcNow;

            if (playerAlive && !monsterAlive)
            {
                //승리
                var winner = s.Participants.First(p => p.Team == TeamSide.Players);
                var player = await _db.Players.FirstAsync(p => p.Id == winner.ActorId, ct);

                var monsterIds = s.Participants.Where(p => p.Team == TeamSide.Monsters).Select(p => p.ActorId).ToArray();
                var mons = await _db.Monsters.Where(m => monsterIds.Contains(m.Id)).ToListAsync(ct);
                int totalExp = mons.Sum(m => m.RewardExp);

                using var tx = await _db.Database.BeginTransactionAsync(ct);
                try
                {
                    _db.RewardGrants.Add(new RewardGrant { BattleId = s.Id, PlayerId = player.Id });
                    player.Exp += totalExp;

                    while (player.Exp >= player.ExpToNext)
                    {
                        player.Exp -= (int)player.ExpToNext;
                        player.Level++;
                        player.ExpToNext = (long)Math.Ceiling(100 * Math.Pow(player.Level, 1.5));
                        player.MaxHP += 10;
                        player.HP = player.MaxHP;
                        player.MaxMP += 5;  player.MP = player.MaxMP;
                    }

                    await _db.SaveChangesAsync(ct);
                    await tx.CommitAsync(ct);
                }
                catch { await tx.RollbackAsync(ct); throw; }
            }
        }

        await _db.SaveChangesAsync(ct);
        return (s.IsFinished, s.Participants.ToList());
    }
}
