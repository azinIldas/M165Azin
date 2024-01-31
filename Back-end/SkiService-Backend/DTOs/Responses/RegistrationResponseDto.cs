using SkiService_Backend.Models;

namespace SkiService_Backend.DTOs.Responses
{
    public class RegistrationResponseDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Priority { get; set; }
        public string Service { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}

