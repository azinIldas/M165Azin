using MongoDB.Driver;
using SkiService_Backend.Models;
using Microsoft.Extensions.Configuration;

namespace SkiService_Backend.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("MongoDB");

            var client = new MongoClient(connectionString);

            _database = client.GetDatabase("M165Azin"); // Der Name Ihrer Datenbank
        }

        public IMongoCollection<Registration> Registrations => _database.GetCollection<Registration>("registrations");

        public IMongoCollection<UserInfo> UserInfos => _database.GetCollection<UserInfo>("userInfo");

        public IMongoCollection<UserSession> UserSessions => _database.GetCollection<UserSession>("userSessions");

        // Weiter Methoden
    }
}
