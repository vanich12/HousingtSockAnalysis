using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? PasswordHash { get; set; }

    public string? AboutMe { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<AddToFavorite> AddToFavorites { get; set; } = new List<AddToFavorite>();
}
