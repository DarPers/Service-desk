﻿namespace Warehouse.BLL.Models;
public class DeviceModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public string? ModelName { get; set; }

    public string? Brand { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? HandedDateTime { get; set; }

    public object? Characteristics { get; set; }
}