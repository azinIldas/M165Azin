using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SkiService_Backend.DTOs.Requests;
using SkiService_Backend.DTOs.Responses;
using SkiService_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly MongoDbContext _context;

    public RegistrationController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistrationResponseDto>>> GetRegistrations()
    {
        var registrations = await _context.Registrations.Find(_ => true).ToListAsync();

        // Konvertierung von Registration zu RegistrationResponseDto
        var registrationDtos = registrations.Select(reg => new RegistrationResponseDto
        {
            ID = reg.Id,
            Name = reg.Name,
            Email = reg.Email,
            Tel = reg.Tel,
            Priority = reg.Priority,
            Service = reg.Service,
            StartDate = reg.StartDate,
            FinishDate = reg.FinishDate,
            Status = reg.Status,
            Note = reg.Note
        }).ToList();

        return Ok(registrationDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Registration>> GetRegistration(string id)
    {
        var registration = await _context.Registrations.Find(r => r.Id == id).FirstOrDefaultAsync();

        if (registration == null)
        {
            return NotFound();
        }

        return registration;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegistration([FromBody] RegistrationDto registrationDto)
    {
        var registration = new Registration
        {
            Name = registrationDto.Name,
            Email = registrationDto.Email,
            Tel = registrationDto.Tel,
            Priority = registrationDto.Priority,
            Service = registrationDto.Service,
            StartDate = registrationDto.StartDate.HasValue ? registrationDto.StartDate.Value : DateTime.UtcNow, 
            FinishDate = registrationDto.FinishDate.HasValue ? registrationDto.FinishDate.Value : DateTime.UtcNow, 
            Status = registrationDto.Status,
            Note = registrationDto.Note
        };

        await _context.Registrations.InsertOneAsync(registration);
        return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistration(string id, [FromBody] RegistrationUpdateRequestDto updateDto)
    {
        var registration = await _context.Registrations.Find(r => r.Id == id).FirstOrDefaultAsync();

        if (registration == null)
        {
            return NotFound();
        }

        // Update the registration with the DTO values
        registration.Name = updateDto.Name;
        registration.Email = updateDto.Email;
        registration.Tel = updateDto.Tel;
        registration.Priority = updateDto.Priority;
        registration.Service = updateDto.Service;
        registration.StartDate = updateDto.StartDate;
        registration.FinishDate = updateDto.FinishDate;
        registration.Status = updateDto.Status;
        registration.Note = updateDto.Note;

        var result = await _context.Registrations.ReplaceOneAsync(r => r.Id == id, registration);

        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            // Create a response DTO
            var responseDto = new RegistrationUpdateResponseDto
            {
                ID = registration.Id,
                Name = registration.Name,
                Email = registration.Email,
                Tel = registration.Tel,
                Priority = registration.Priority,
                Service = registration.Service,
                StartDate = registration.StartDate,
                FinishDate = registration.FinishDate,
                Status = registration.Status,
                Note = registration.Note
            };

            return Ok(responseDto);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(string id)
    {
        var result = await _context.Registrations.DeleteOneAsync(r => r.Id == id);

        if (result.IsAcknowledged && result.DeletedCount > 0)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    private bool RegistrationExists(string id)
    {
        return _context.Registrations.Find(r => r.Id == id).Any();
    }
}
