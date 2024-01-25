using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.CarService.DataLayer.Models;

public partial class DealerApisemiFinalContext : DbContext
{
    public DealerApisemiFinalContext()
    {
    }

    public DealerApisemiFinalContext(DbContextOptions<DealerApisemiFinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Cars_UserInfoId");

            entity.Property(e => e.CarId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
