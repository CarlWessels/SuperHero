using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SuperHero.API.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHero.API.Utils
{
    public class AuthUtils(IOptions<AppSettings> appSettings) : IAuthUtils
    {
        private readonly AppSettings _appSettings = appSettings.Value;
        public string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _appSettings.SecretKey;
            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("SecretKey is missing in appsettings.json");
            
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public byte[] HashPassword(string password)
        {
            //THIS IS TOO SIMPLE HASHING FOR PRODUCTION, WOULD USE A PROPER IMPLEMENTATION HERE, USING THIS FOR SIMPLICITY!!!!!
            var hashedBytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return hashedBytes;
        }
    }
}
