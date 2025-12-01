using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Gamza.Services // 네임스페이스가 있다면 맞춰주세요
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(int userId, string role)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                // 필요하다면 실제 로그인 아이디(string)를 넣어도 됩니다. 지금은 PK(int)를 넣고 있습니다.
                new Claim("LoginId", userId.ToString()), 
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        
            var minutesStr = _config["Jwt:ExpireMinutes"]; // JSON 키 이름 확인 필수!
            
            if (!int.TryParse(minutesStr, out int minutes) || minutes <= 0)
            {
                minutes = 60; // 파싱 실패하거나 0 이하일 경우 기본값
            }

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(minutes), // 이제 절대 0분이 되지 않습니다.
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}