using AutoTechAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoTechAPI.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes("x+d3Bx+a2PBr+w3dY+QyGpUz3zI0imvkhYbsQGpQ4kF0KPFP6rhL2H8lhznsTSfgUkKSm99FVJc6YN6eTcqYpw==");
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("UserId", user.Id.ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }

    }
}
