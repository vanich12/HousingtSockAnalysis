using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared;

[Table("userinput")]
public partial class Userinput
{
    [Key]
    [Column("input_id")]
    public int InputId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("property_id")]
    public int? PropertyId { get; set; }

    [Column("estimated_rent")]
    [Precision(10, 2)]
    public decimal? EstimatedRent { get; set; }

    [Column("estimated_expenses")]
    [Precision(10, 2)]
    public decimal? EstimatedExpenses { get; set; }

    [Column("investment_amount")]
    [Precision(10, 2)]
    public decimal? InvestmentAmount { get; set; }

    [Column("expected_return")]
    [Precision(5, 2)]
    public decimal? ExpectedReturn { get; set; }

    [Column("calculation_date", TypeName = "timestamp without time zone")]
    public DateTime? CalculationDate { get; set; }

    [InverseProperty("Input")]
    public virtual ICollection<Liquidityresult> Liquidityresults { get; set; } = new List<Liquidityresult>();

    [ForeignKey("PropertyId")]
    [InverseProperty("Userinputs")]
    public virtual Property? Property { get; set; }
}
