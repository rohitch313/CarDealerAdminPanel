using System;
using System.Collections.Generic;
using AdminService.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminService.DataLayer;

public partial class DealerApifinalContext : DbContext
{
    public DealerApifinalContext()
    {
    }

    public DealerApifinalContext(DbContextOptions<DealerApifinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminTable> AdminTables { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminTable>(entity =>
        {
            entity.ToTable("AdminTable");

            entity.Property(e => e.PasswordHast).HasMaxLength(260);
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(260)
                .HasColumnName("passwordSalt");
            entity.Property(e => e.TokenCreated)
                .HasColumnType("datetime")
                .HasColumnName("tokenCreated");
            entity.Property(e => e.TokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
