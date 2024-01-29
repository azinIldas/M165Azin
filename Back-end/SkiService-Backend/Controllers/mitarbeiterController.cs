using Microsoft.AspNetCore.Mvc;
using SkiService_Backend.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SkiService_Backend.Controllers
{
    [Route("api/mitarbeiter")]
    [ApiController]
    public class mitarbeiterController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly IConfiguration _configuration;

        public mitarbeiterController(MongoDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> PostDashboard([FromBody] mitarbeitermodel dashboardModel)
        {
            var userSession = await _context.UserSessions
                                            .Find(us => us.SessionKey == dashboardModel.Token)
                                            .FirstOrDefaultAsync();

            if (userSession != null)
            {
                var registrations = await _context.Registrations
                                                  .Find(_ => true)
                                                  .ToListAsync();
                return Ok(registrations);
            }
            else
            {
                return BadRequest("Session invalid");
            }
        }

        [HttpPut("registration/{id}")]
        public async Task<IActionResult> PutRegistration(string id, [FromBody] Registration registration, string token)
        {
            var userSession = await _context.UserSessions
                                            .Find(us => us.SessionKey == token)
                                            .FirstOrDefaultAsync();
            if (userSession == null)
            {
                return BadRequest("Invalid session token");
            }

            var filter = Builders<Registration>.Filter.Eq(r => r.Id, id);
            var updateResult = await _context.Registrations
                                             .ReplaceOneAsync(filter, registration, new ReplaceOptions { IsUpsert = true });

            if (updateResult.IsAcknowledged && updateResult.ModifiedCount == 0)
            {
                return NotFound();
            }

            return Ok(registration);
        }

        [HttpDelete("registration/{id}")]
        public async Task<IActionResult> DeleteRegistration(string id, string token)
        {
            var userSession = await _context.UserSessions
                                            .Find(us => us.SessionKey == token)
                                            .FirstOrDefaultAsync();
            if (userSession == null)
            {
                return BadRequest("Invalid session token");
            }

            var filter = Builders<Registration>.Filter.Eq(r => r.Id, id);
            var deleteResult = await _context.Registrations.DeleteOneAsync(filter);

            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> RegistrationExists(string id)
        {
            var registration = await _context.Registrations
                                             .Find(r => r.Id == id)
                                             .FirstOrDefaultAsync();
            return registration != null;
        }
    }
}