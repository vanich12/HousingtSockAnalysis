using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class Liquidityresult
{
    public int ResultId { get; set; }

    public int? InputId { get; set; }

    public decimal? LiquidityScore { get; set; }

    public decimal? NetIncome { get; set; }

    public decimal? CashFlow { get; set; }

    public decimal? Roi { get; set; }

    public DateTime? CalculationDate { get; set; }

    public virtual Userinput? Input { get; set; }
}
