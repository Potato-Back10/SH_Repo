using Gamza.Data;
using Gamza.Models; // User 모델이 있는 곳
using Gamza.Enums;  // UserRole이 있는 곳
using Microsoft.EntityFrameworkCore;

namespace Gamza.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public AuthService(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<bool> RegisterAsync(string loginId, string password, string username)
        {

            bool exists = await _context.Users.AnyAsync(u => u.LoginID == loginId);
            if (exists) return false;

            var newUser = new User
            {
                LoginID = loginId,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Username = username, 
                Role = "User"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string?> LoginAsync(string loginId, string password, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.LoginID == loginId, ct);
            if (user is null) return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) return null;

            return _jwt.GenerateToken(user.Id, user.Role);
        }
    }
}