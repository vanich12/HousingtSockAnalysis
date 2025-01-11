using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Table("liquidityresults")]
public partial class Liquidityresult
{
    [Key]
    [Column("result_id")]
    public int ResultId { get; set; }

    [Column("input_id")]
    public int? InputId { get; set; }

    [Column("liquidity_score")]
    [Precision(10, 2)]
    public decimal? LiquidityScore { get; set; }

    [Column("net_income")]
    [Precision(10, 2)]
    public decimal? NetIncome { get; set; }

    [Column("cash_flow")]
    [Precision(10, 2)]
    public decimal? CashFlow { get; set; }

    [Column("roi")]
    [Precision(5, 2)]
    public decimal? Roi { get; set; }

    [Column("calculation_date", TypeName = "timestamp without time zone")]
    public DateTime? CalculationDate { get; set; }

    [ForeignKey("InputId")]
    [InverseProperty("Liquidityresults")]
    public virtual Userinput? Input { get; set; }
}
