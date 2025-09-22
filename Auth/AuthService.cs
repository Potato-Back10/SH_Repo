using Gamza.Data;
using Gamza.Dtos;
using Gamza.Models;
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

        public async Task<string?> LoginAsync(
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

            return _jwt.GenerateToken(user.Id, user.Role);
        }
    }
}
