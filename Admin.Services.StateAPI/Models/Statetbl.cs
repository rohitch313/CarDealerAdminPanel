using System;
using System.Collections.Generic;

namespace Admin.Services.StateAPI.Models;

public partial class Statetbl
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public string StateCode { get; set; } = null!;
}
