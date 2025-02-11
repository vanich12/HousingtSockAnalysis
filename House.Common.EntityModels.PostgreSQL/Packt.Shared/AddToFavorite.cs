using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class AddToFavorite
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OfferId { get; set; }

    public DateOnly? DateAdded { get; set; }

    public virtual Offer Offer { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
