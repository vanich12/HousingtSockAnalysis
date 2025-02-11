using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class LivingComplex
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FullUrl { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
}
