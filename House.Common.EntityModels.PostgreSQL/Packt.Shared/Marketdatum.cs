using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class Marketdatum
{
    public string? City { get; set; }

    public string? State { get; set; }

    public decimal? AverageRent { get; set; }

    public int? RentalDemand { get; set; }

    public decimal? VacancyRate { get; set; }

    public decimal? GrowthRate { get; set; }

    public DateOnly? Date { get; set; }

    public string Id { get; set; } = null!;
}
