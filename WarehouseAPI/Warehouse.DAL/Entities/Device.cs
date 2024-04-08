using MongoDB.Bson.Serialization.Attributes;

namespace Warehouse.DAL.Entities;

[BsonIgnoreExtraElements]
public class Device
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public string? ModelName { get; set; }

    public string? Brand { get; set; }

    public string? Characteristics { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? HandedDateTime { get; set; }
}
