using System;
using System.Collections.Generic;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class Offer
{
    public int Id { get; set; }

    public int AddressId { get; set; }

    public string? ZipCode { get; set; }

    public decimal? LivingArea { get; set; }

    public decimal? KitchenArea { get; set; }

    public int? YuarBuilt { get; set; }

    public string? Description { get; set; }

    public string? TypeofHousing { get; set; }

    public int? RoomsCount { get; set; }

    public bool? NewBuilding { get; set; }

    public decimal? FullPrice { get; set; }

    public bool? WithFinishing { get; set; }

    public string? Photos { get; set; }

    public int Jk { get; set; }

    public virtual ICollection<AddToFavorite> AddToFavorites { get; set; } = new List<AddToFavorite>();

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<HistoryOfChange> HistoryOfChanges { get; set; } = new List<HistoryOfChange>();

    public virtual LivingComplex JkNavigation { get; set; } = null!;

    public virtual TypeofHousing? TypeofHousingNavigation { get; set; }
}
