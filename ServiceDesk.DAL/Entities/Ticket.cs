﻿using ServiceDesk.Domain.Enums;

namespace ServiceDesk.DAL.Entities;

public class Ticket : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Status Status { get; set; }

    public DateTime? DateTimeAccepted { get; set; }
}