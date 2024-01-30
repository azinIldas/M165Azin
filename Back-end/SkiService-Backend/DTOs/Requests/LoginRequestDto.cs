using SkiService_Backend.Models;


namespace SkiService_Backend.DTOs.Requests
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

