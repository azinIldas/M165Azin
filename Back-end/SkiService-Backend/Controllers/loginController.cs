﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkiService_Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace SkiService_Backend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(MongoDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostLogin(LoginModel loginModel)
        {
            var userInfo = await _context.UserInfos.Find(u => u.UserName == loginModel.UserName).FirstOrDefaultAsync();

            if (userInfo != null && userInfo.Password == loginModel.Password)
            {
                var token = GenerateJwtToken(userInfo);
                var userSession = new UserSession
                {
                    SessionKey = token,
                    UserId = userInfo.Id.ToString(), 
                };
                await _context.UserSessions.InsertOneAsync(userSession);

                return Ok(token);
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
