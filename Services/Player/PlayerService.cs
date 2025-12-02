using Gamza.Data;
using Gamza.Dtos;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamza.Services
{
    public class PlayerService
    {
        private readonly AppDbContext _db;

        public PlayerService(AppDbContext db)
        {
            _db = db;
        }
        // 생성
        public async Task<List<Player>> GetMyPlayersAsync(
            int userId,
            CancellationToken ct = default)
        {
            return await _db.Players
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Id)
                .ToListAsync(ct);
        }

        // 내 캐릭터 하나 조회
        public async Task<Player?> GetMyPlayerAsync(
            int userId,
            int playerId,
            CancellationToken ct = default)
        {
            return await _db.Players
                .FirstOrDefaultAsync(p => p.Id == playerId && p.UserId == userId, ct);
        }

        // 캐릭터 생성
        public async Task<Player> CreatePlayerAsync(
            int userId,
            PlayerCreateDto dto,
            CancellationToken ct = default)
        {
            // 닉네임 중복 체크
            if (await _db.Players.AnyAsync(p => p.NickName == dto.NickName, ct))
                throw new Exception("이미 사용 중인 닉네임입니다.");

            var player = new Player
            {
                NickName = dto.NickName,
                Level = 1,
                Exp = 0,
                Job = dto.Job,
                PlayerStauts = dto.PlayerStauts,
                UserId = userId
            };

            _db.Players.Add(player);
            await _db.SaveChangesAsync(ct);

            return player;
        }

        // 캐릭터 삭제
        public async Task DeleteMyPlayerAsync(
            int userId,
            int playerId,
            CancellationToken ct = default)
        {
            var player = await GetMyPlayerAsync(userId, playerId, ct);
            if (player is null)
                throw new Exception("캐릭터를 찾을 수 없습니다.");

            _db.Players.Remove(player);
            await _db.SaveChangesAsync(ct);
        }
    }
}
