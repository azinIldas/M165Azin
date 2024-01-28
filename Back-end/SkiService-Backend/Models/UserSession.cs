using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkiService_Backend.Models;

/// <summary>
/// Model für sessions der Mitarbeiter.
/// Angepasst für die Verwendung mit MongoDB.
/// </summary>
public class UserSession
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("sessionKey")]
    public string SessionKey { get; set; }

    // Für die Referenzierung zu einem User-Dokument speichern Sie die ObjectId des Users.
    // Dies entspricht einer ForeignKey-Beziehung in relationalen Datenbanken.
    [BsonElement("userId")]
    public string UserId { get; set; }
    
}
