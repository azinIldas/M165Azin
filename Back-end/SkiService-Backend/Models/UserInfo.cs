using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkiService_Backend.Models;

/// <summary>
/// Model für die Mitarbeiter registration
/// Angepasst für die Verwendung mit MongoDB.
/// </summary>
public class UserInfo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("ID")]
    public int CustomId { get; set; }

    [BsonElement("userName")]
    public string UserName { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

}
