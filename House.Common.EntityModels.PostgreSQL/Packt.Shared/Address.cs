using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class Address
{
    public int Id { get; set; }

    public string? City { get; set; }

    public int DistrictId { get; set; }

    public string? Street { get; set; }

    public string? HouseNumber { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
