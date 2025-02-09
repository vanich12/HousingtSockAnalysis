using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class Userinput
{
    public int InputId { get; set; }

    public int? UserId { get; set; }

    public int? PropertyId { get; set; }

    public decimal? EstimatedRent { get; set; }

    public decimal? EstimatedExpenses { get; set; }

    public decimal? InvestmentAmount { get; set; }

    public decimal? ExpectedReturn { get; set; }

    public DateTime? CalculationDate { get; set; }

    public virtual ICollection<Liquidityresult> Liquidityresults { get; set; } = new List<Liquidityresult>();

    public virtual Property? Property { get; set; }
}
