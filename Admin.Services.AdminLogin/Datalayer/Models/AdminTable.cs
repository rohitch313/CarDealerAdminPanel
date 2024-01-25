using System;
using System.Collections.Generic;

namespace AdminService.DataLayer.Models;

public partial class AdminTable
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public byte[] PasswordHast { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public DateTime? TokenExpiry { get; set; }

    public DateTime? TokenCreated { get; set; }
}
