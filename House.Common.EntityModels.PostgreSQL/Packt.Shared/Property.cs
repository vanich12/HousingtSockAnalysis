using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Table("properties")]
public partial class Property
{
    [Key]
    [Column("property_id")]
    public int PropertyId { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("city")]
    [StringLength(100)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(100)]
    public string? State { get; set; }

    [Column("zip_code")]
    [StringLength(20)]
    public string? ZipCode { get; set; }

    [Column("bedrooms")]
    public int? Bedrooms { get; set; }

    [Column("bathrooms")]
    public int? Bathrooms { get; set; }

    [Column("area_sqm")]
    [Precision(10, 2)]
    public decimal? AreaSqm { get; set; }

    [Column("year_built")]
    public int? YearBuilt { get; set; }

    [Column("property_type")]
    [StringLength(100)]
    public string? PropertyType { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("price_per_month")]
    [Precision(10, 2)]
    public decimal? PricePerMonth { get; set; }

    [Column("maintenance_fees")]
    [Precision(10, 2)]
    public decimal? MaintenanceFees { get; set; }

    [Column("taxes")]
    [Precision(10, 2)]
    public decimal? Taxes { get; set; }

    [Column("insurance")]
    [Precision(10, 2)]
    public decimal? Insurance { get; set; }

    [Column("utilities")]
    [Precision(10, 2)]
    public decimal? Utilities { get; set; }

    [Column("additional_costs")]
    [Precision(10, 2)]
    public decimal? AdditionalCosts { get; set; }

    [InverseProperty("Property")]
    public virtual ICollection<Userinput> Userinputs { get; set; } = new List<Userinput>();
}
