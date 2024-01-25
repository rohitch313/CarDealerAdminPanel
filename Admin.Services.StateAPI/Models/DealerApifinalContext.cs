using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.StateAPI.Models;

public partial class DealerApifinalContext : DbContext
{
    public DealerApifinalContext()
    {
    }

    public DealerApifinalContext(DbContextOptions<DealerApifinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Statetbl> Statetbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=EAT-LTP101;Database=DealerAPIFinal;User ID=sa;Password=password;Encrypt=False;TrustServerCertificate=True;Language=us_english");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Statetbl>(entity =>
        {
            entity.HasKey(e => e.StateId);

            entity.ToTable("Statetbl");

            entity.Property(e => e.StateCode).HasMaxLength(14);
            entity.Property(e => e.StateName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
