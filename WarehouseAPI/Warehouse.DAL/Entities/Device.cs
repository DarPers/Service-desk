using MongoDB.Bson.Serialization.Attributes;
using Warehouse.DAL.Attributes;

namespace Warehouse.DAL.Entities;

[BsonIgnoreExtraElements]
[BsonCollection("Devices")]
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
