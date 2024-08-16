using AspDotNetCoreProject.Context.Account;
using AspDotNetCoreProject.Context.Common;
using AspDotNetCoreProject.Context.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspDotNetCoreProject.Application
{
    public class AccountService : IAccountService
    {
        private readonly JwtConfig _jwtSettings;
        private readonly IUserRepository _userRepository;

        public AccountService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _jwtSettings = appSettings.Value.JwtConfig;
            _userRepository = userRepository;
        }

        public async Task<long> Register(UserEntity user)
        {
            var existingUser = await _userRepository.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                return -1;
            }
            user.Password = HashPassword(user.Password);
            user.CreatedOn = DateTime.UtcNow;
            user.CreatedBy = "AdminUserId";
            return await _userRepository.CreateUser(user);
        }

        public async Task<UserEntity?> Login(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            var isPasswordValid = user != null && VerifyPassword(user.Password, password);
            return isPasswordValid ? user : null;
        }

        public string GenerateJwtToken(UserEntity user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
    }
}
