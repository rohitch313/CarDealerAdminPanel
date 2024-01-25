using System;
using System.Collections.Generic;

namespace Admin.Services.CustomerSupport.Models;

public partial class CustomerSupporttbl
{
    public int Id { get; set; }

    public string Call { get; set; } = null!;

    public string WhatsApp { get; set; } = null!;

    public string Email { get; set; } = null!;
}
