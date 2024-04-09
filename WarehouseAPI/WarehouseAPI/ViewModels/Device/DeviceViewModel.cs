namespace WarehouseAPI.ViewModels.Device;

public class DeviceViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string SerialNumber { get; set; }

    public string ModelName { get; set; }

    public Guid? UserId { get; set; }

    public object Characteristics { get; set; }

    public DateTime? HandedDateTime { get; set; }
}
