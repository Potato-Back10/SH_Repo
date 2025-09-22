using Gamza.Data;
using Gamza.Dtos;
using Gamza.Extensions;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;
using UserEntity = Gamza.Models.User;

namespace Gamza.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto> Register(
            UserRequestDto request,
            CancellationToken ct = default
        )
        {
            if (await _context.Users.AnyAsync(u => u.LoginID == request.LoginID, ct))
                throw new Exception("이미 등록된 아이디입니다.");

            var user = new User
            {
                Username = request.Username,
                LoginID = request.LoginID,
                Password = request.Password,
                Role = "User",
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(ct);

            return user.ToResponseDto();
        }

        public async Task<User> FindByUserIdAsync(int userId, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
            if (user is null)
                throw new KeyNotFoundException("사용자를 찾을 수 없습니다.");
            return user;
        }

        public async Task<UserResponseDto?> UpdateAsync(
            int id,
            UserRequestDto request,
            CancellationToken ct = default
        )
        {
            var user = await _context.Users.FindAsync(new object?[] { id }, ct);
            if (user is null)
                return null;

            user.Username = request.Username;
            user.LoginID = request.LoginID;

            await _context.SaveChangesAsync(ct);

            return user.ToResponseDto();
        }

        public async Task<bool> DeleteUserAsync(int id, CancellationToken ct = default)
        {
            var user = await _context.Users.FindAsync(new object?[] { id }, ct);
            if (user is null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<UserResponseDto>> AllUsersRetrieveAsync(
            CancellationToken ct = default
        )
        {
            return await _context.Users.Select(u => u.ToResponseDto()).ToListAsync(ct);
        }

        public async Task<UserResponseDto?> OneUserRetrieveAsync(
            int id,
            CancellationToken ct = default
        )
        {
            var user = await _context.Users.FindAsync(new object?[] { id }, ct);
            if (user is null)
                return null;

            return user.ToResponseDto();
        }
    }
}
