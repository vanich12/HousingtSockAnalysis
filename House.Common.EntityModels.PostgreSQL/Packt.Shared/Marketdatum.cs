using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Table("marketdata")]
public partial class Marketdatum
{
    [Key]
    [Column("market_data_id")]
    public int MarketDataId { get; set; }

    [Column("city")]
    [StringLength(100)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(100)]
    public string? State { get; set; }

    [Column("average_rent")]
    [Precision(10, 2)]
    public decimal? AverageRent { get; set; }

    [Column("rental_demand")]
    public int? RentalDemand { get; set; }

    [Column("vacancy_rate")]
    [Precision(5, 2)]
    public decimal? VacancyRate { get; set; }

    [Column("growth_rate")]
    [Precision(5, 2)]
    public decimal? GrowthRate { get; set; }

    [Column("date")]
    public DateOnly? Date { get; set; }
}
