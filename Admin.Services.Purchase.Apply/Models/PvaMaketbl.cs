using System;
using System.Collections.Generic;

namespace Admin.Services.Purchase.Apply.Models;

public partial class PvaMaketbl
{
    public int MakeId { get; set; }

    public string MakeName { get; set; } = null!;

    public int MakeCode { get; set; }

    public virtual ICollection<PvAggregatorstbl> PvAggregatorstbls { get; set; } = new List<PvAggregatorstbl>();
}
