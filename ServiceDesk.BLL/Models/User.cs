﻿using ServiceDesk.Domain.Enums;

namespace ServiceDesk.BLL.Models;
public class User : BaseModel
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Role Role { get; set; }
}
