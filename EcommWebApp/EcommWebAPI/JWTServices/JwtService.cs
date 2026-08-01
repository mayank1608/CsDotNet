using EcommWebAPI.Models;
using EcommWebAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EcommWebAPI.Models.User;

namespace EcommWebAPI.JWTServices
{
    public class JwtService
    {
        private readonly EcommDbContext _context;
        private readonly IConfiguration _config;
        public JwtService(EcommDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<LogInResponseModel?> Authenticate(LogInRequestModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                    return null;
                var userAccount = await _context.UserAccounts.FirstOrDefaultAsync(x => x.UserName == request.UserName);

                if (userAccount is null || !PasswordHashHandler.VerifyPassword(request.Password, userAccount.Password))
                    return null;

                var issuer = _config["JwtConfig:Issuer"];
                var audience = _config["JwtConfig:Audience"];
                var key = _config["JwtConfig:Key"];
                var tokenValidityMins = _config.GetValue<int>("JwtConfig:TokenValidityMins");
                var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Name, request.UserName)
                    }),
                    Expires = tokenExpiryTimeStamp,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        SecurityAlgorithms.HmacSha512Signature
                    )

                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(securityToken);

                return new LogInResponseModel
                {
                    AccessToken = accessToken,
                    UserName = request.UserName,
                    ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
