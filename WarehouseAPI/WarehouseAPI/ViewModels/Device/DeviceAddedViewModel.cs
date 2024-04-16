namespace WarehouseAPI.ViewModels.Device;

public class DeviceAddedViewModel
{
    public string Name { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public string? ModelName { get; set; }

    public string? Brand { get; set; }

    public object? Characteristics { get; set; }
}
