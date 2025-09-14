using Gamza.Data;
using Gamza.Dtos;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;
using UserEntity = Gamza.Models.User;

namespace Gamza.Services
{
    public class UpdateUserService
    {
        private readonly AppDbContext _context;

        public UpdateUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto?> updated(int id, UserRequestDto request)
        {
            if (await _context.Users.FindAsync(id) is not User user)
                return null;

            user.Username = request.Username;
            user.UserID = request.UserID;
            user.Job = request.Job;

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Username = request.Username,
                UserID = request.UserID,
                Job = request.Job,
            };
        }

        public async Task<bool> DeleteUser(int id)
        {
            if (await _context.Users.FindAsync(id) is not User user)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
