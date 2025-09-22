using Gamza.Data;
using Gamza.Dtos;
using Gamza.Models;
using Microsoft.EntityFrameworkCore;
using UserEntity = Gamza.Models.User;

namespace Gamza.Services
{
    public class RegisterSerivce
    {
        private readonly AppDbContext _context;

        public RegisterSerivce(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDto> Register(UserRequestDto request)
        {
            if (await _context.Users.AnyAsync(u => u.UserID == request.UserID))
            {
                throw new Exception("이미 등록된 아이디입니다.");
            }

            UserEntity user = new UserEntity
            {
                Username = request.Username,
                UserID = request.UserID,
                Job = request.Job,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Username = user.Username,
                UserID = user.UserID,
                Job = user.Job,
            };
        }
    }
}
