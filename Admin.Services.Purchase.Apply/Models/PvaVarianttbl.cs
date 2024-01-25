using System;
using System.Collections.Generic;

namespace Admin.Services.Purchase.Apply.Models;

public partial class PvaVarianttbl
{
    public int VariantId { get; set; }

    public string VariantName { get; set; } = null!;

    public int VariantCode { get; set; }

    public virtual ICollection<PvAggregatorstbl> PvAggregatorstbls { get; set; } = new List<PvAggregatorstbl>();
}
