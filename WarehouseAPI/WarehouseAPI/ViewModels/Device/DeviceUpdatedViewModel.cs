namespace WarehouseAPI.ViewModels.Device;

public class DeviceUpdatedViewModel
{
    public string Name { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public string? Brand { get; set; }

    public string? ModelName { get; set; }

    public object? Characteristics { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? HandedDateTime { get; set; }
}
