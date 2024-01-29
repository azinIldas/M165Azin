using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
    public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
    {
        return await _context.Registrations.Find(_ => true).ToListAsync();
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
    public async Task<ActionResult<Registration>> PostRegistration(Registration registration)
    {
        await _context.Registrations.InsertOneAsync(registration);
        return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistration(string id, Registration registration)
    {
        if (id != registration.Id)
        {
            return BadRequest();
        }

        var result = await _context.Registrations.ReplaceOneAsync(r => r.Id == id, registration);
        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            return NoContent();
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
