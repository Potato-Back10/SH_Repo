using Gamza.Dtos;
using Gamza.Models;

namespace Gamza.Extensions
{
    public static class UserExtensions
    {
        public static UserResponseDto ToResponseDto(this User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                LoginID = user.LoginID,
            };
        }
    }
}
