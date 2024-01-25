using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.CustomerSupport.Models;

public partial class DealerApifinalContext : DbContext
{
    public DealerApifinalContext()
    {
    }

    public DealerApifinalContext(DbContextOptions<DealerApifinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerSupporttbl> CustomerSupporttbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=EAT-LTP101;Database=DealerAPIFinal;User ID=sa;Password=password;Encrypt=False;TrustServerCertificate=True;Language=us_english");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerSupporttbl>(entity =>
        {
            entity.ToTable("CustomerSupporttbl");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
