using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class User
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public int AccessFailedCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}
