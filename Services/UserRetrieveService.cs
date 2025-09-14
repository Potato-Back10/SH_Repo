using Gamza.Data;
using Gamza.Dtos;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;
using UserEntity = Gamza.Models.User;

namespace Gamza.Services
{
    public class UserRetrieveSerivce
    {
        private readonly AppDbContext _context;

        public UserRetrieveSerivce(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserResponseDto>> AllUsersRetrieve()
        {
            return await _context
                .Users.Select(u => new UserResponseDto
                {
                    Username = u.Username,
                    UserID = u.UserID,
                    Job = u.Job,
                })
                .ToListAsync();
        }

        public async Task<UserResponseDto?> OneUsersRetrieve(int id)
        {
            if (await _context.Users.FindAsync(id) is not User user)
                return null;
            return new UserResponseDto
            {
                Username = user.Username,
                UserID = user.UserID,
                Job = user.Job,
            };
        }
    }
}
