using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class HistoryOfChange
{
    public int Id { get; set; }

    public int OfferId { get; set; }

    public decimal? Price { get; set; }

    public decimal? PerMeterPrice { get; set; }

    public virtual Offer Offer { get; set; } = null!;
}
