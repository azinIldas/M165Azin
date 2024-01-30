using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkiService_Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;
using SkiService_Backend.Services;
using SkiService_Backend.DTOs.Requests;



namespace SkiService_Backend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public AuthenticationController(MongoDbContext context, IConfiguration configuration, TokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var userInfo = await _context.UserInfos
                .Find(u => u.UserName == loginRequest.UserName)
                .FirstOrDefaultAsync();

            if (userInfo != null && loginRequest.Password == userInfo.Password)
            {
                var token = GenerateJwtToken(userInfo);
                var userSession = new UserSession
                {
                    SessionKey = token,
                    UserId = userInfo.Id, // Stellen Sie sicher, dass der Typ übereinstimmt
                };
                await _context.UserSessions.InsertOneAsync(userSession);

                // Sie können auch ein Response-Objekt zurückgeben, das den Token und zusätzliche Informationen enthält
                return Ok(new { Token = token, UserName = userInfo.UserName });
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

        private string GenerateJwtToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
