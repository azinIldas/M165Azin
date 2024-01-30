namespace SkiService_Backend.DTOs.Requests;

public class RegistrationDto
{
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
