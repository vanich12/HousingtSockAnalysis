using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class District
{
    public int Id { get; set; }

    public decimal? AvarageOfferPrice { get; set; }

    public decimal? AvaragePerMeterPrice { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
