using SkiService_Backend.Models;
using SkiService_Backend.DTOs.Requests;

namespace SkiService_Backend.DTOs.Responses
{
    public class MitarbeiterResponseDto
    {
        public List<RegistrationDto> Registrations { get; set; }
    }
}
