using System;
using System.Collections.Generic;

namespace Admin.Services.Purchase.Apply.Models;

public partial class PvAggregatorstbl
{
    public int Id { get; set; }

    public string PurchaseAmount { get; set; } = null!;

    public int MakeId { get; set; }

    public int ModelId { get; set; }

    public int YearOfRegistration { get; set; }

    public int VariantId { get; set; }

    public string PriceBreak { get; set; } = null!;

    public string StockIn { get; set; } = null!;

    public string Rcavailable { get; set; } = null!;

    public int UserInfoId { get; set; }

    public virtual PvaMaketbl Make { get; set; } = null!;

    public virtual PvaModeltbl Model { get; set; } = null!;

    public virtual PvaVarianttbl Variant { get; set; } = null!;

    public virtual PvaYearOfRegtbl YearOfRegistrationNavigation { get; set; } = null!;
}
