using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkiService_Backend.Models;

public class Registration
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("ID")]
    public int CustomId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("tel")]
    public string Tel { get; set; }

    [BsonElement("priority")]
    public string Priority { get; set; }

    [BsonElement("service")]
    public string Service { get; set; }

    [BsonElement("startDate")]
    public DateTime? StartDate { get; set; }

    [BsonElement("finishDate")]
    public DateTime FinishDate { get; set; }

    [BsonElement("status")]
    public string? Status { get; set; }

    [BsonElement("note")]
    public string? Note { get; set; }
}
