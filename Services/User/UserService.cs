using Gamza.Data;
using Gamza.Dtos;
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

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                LoginID = user.LoginID,
            };
        }

        public async Task<string> Login(
            LoginRequestDto dto,
            JwtService jwt,
            CancellationToken ct = default
        )
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.LoginID == dto.LoginID, ct);
            if (user is null)
                throw new UnauthorizedAccessException("아이디 또는 비밀번호가 올바르지 않습니다.");

            if (user.Password != dto.Password)
                throw new UnauthorizedAccessException("아이디 또는 비밀번호가 올바르지 않습니다.");

            return jwt.GenerateToken(user.Id, "User");
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

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                LoginID = user.LoginID,
            };
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
            return await _context
                .Users.Select(u => new UserResponseDto { Id = u.Id, Username = u.Username })
                .ToListAsync(ct);
        }

        public async Task<UserResponseDto?> OneUserRetrieveAsync(
            int id,
            CancellationToken ct = default
        )
        {
            var user = await _context.Users.FindAsync(new object?[] { id }, ct);
            if (user is null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                LoginID = user.LoginID,
            };
        }

        public async Task<User?> ValidateUserAsync(
            string loginId,
            string password,
            CancellationToken ct = default
        )
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.LoginID == loginId, ct);
            if (user is null)
                return null;

            if (user.Password != password)
                return null;

            return user;
        }
    }
}
