using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkiService_Backend.Models
{
    public class LoginModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userName")]
        [BsonRequired]
        public string UserName { get; set; }

        [BsonElement("password")]
        [BsonRequired]
        public string Password { get; set; }
    }
}
