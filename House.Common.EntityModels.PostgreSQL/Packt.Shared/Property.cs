using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class Property
{
    public int PropertyId { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public int? Bedrooms { get; set; }

    public int? Bathrooms { get; set; }

    public decimal? AreaSqm { get; set; }

    public int? YearBuilt { get; set; }

    public string? PropertyType { get; set; }

    public string? Description { get; set; }

    public decimal? PricePerMonth { get; set; }

    public decimal? MaintenanceFees { get; set; }

    public decimal? Taxes { get; set; }

    public decimal? Insurance { get; set; }

    public decimal? Utilities { get; set; }

    public decimal? AdditionalCosts { get; set; }

    public virtual ICollection<Userinput> Userinputs { get; set; } = new List<Userinput>();
}
