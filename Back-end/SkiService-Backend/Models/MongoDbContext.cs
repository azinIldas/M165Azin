using MongoDB.Driver;
using SkiService_Backend.Models;

namespace SkiService_Backend.Models
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient client, string databaseName)
        {
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Registration> Registrations => _database.GetCollection<Registration>("Registrations");
        public IMongoCollection<UserInfo> UserInfos => _database.GetCollection<UserInfo>("UserInfos");
        public IMongoCollection<UserSession> UserSessions => _database.GetCollection<UserSession>("UserSessions");

    }
}
