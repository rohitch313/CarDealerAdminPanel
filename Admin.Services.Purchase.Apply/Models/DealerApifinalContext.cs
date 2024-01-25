using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.Purchase.Apply.Models;

public partial class DealerApifinalContext : DbContext
{
    public DealerApifinalContext()
    {
    }

    public DealerApifinalContext(DbContextOptions<DealerApifinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PvAggregatorstbl> PvAggregatorstbls { get; set; }

    public virtual DbSet<PvNewCarDealerstbl> PvNewCarDealerstbls { get; set; }

    public virtual DbSet<PvOpenMarketstbl> PvOpenMarketstbls { get; set; }

    public virtual DbSet<PvaMaketbl> PvaMaketbls { get; set; }

    public virtual DbSet<PvaModeltbl> PvaModeltbls { get; set; }

    public virtual DbSet<PvaVarianttbl> PvaVarianttbls { get; set; }

    public virtual DbSet<PvaYearOfRegtbl> PvaYearOfRegtbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 => optionsBuilder.UseSqlServer("Server=EAT-LTP101;Database=DealerAPIFinal;User ID=sa;Password=password;Encrypt=False;TrustServerCertificate=True;Language=us_english");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PvAggregatorstbl>(entity =>
        {
            entity.ToTable("PV_Aggregatorstbl");

            entity.HasIndex(e => e.MakeId, "IX_PV_Aggregatorstbl_MakeId");

            entity.HasIndex(e => e.ModelId, "IX_PV_Aggregatorstbl_ModelId");

            entity.HasIndex(e => e.UserInfoId, "IX_PV_Aggregatorstbl_UserInfoId");

            entity.HasIndex(e => e.VariantId, "IX_PV_Aggregatorstbl_VariantId");

            entity.HasIndex(e => e.YearOfRegistration, "IX_PV_Aggregatorstbl_YearOfRegistration");

            entity.Property(e => e.Rcavailable).HasColumnName("RCAvailable");

            entity.HasOne(d => d.Make).WithMany(p => p.PvAggregatorstbls).HasForeignKey(d => d.MakeId);

            entity.HasOne(d => d.Model).WithMany(p => p.PvAggregatorstbls).HasForeignKey(d => d.ModelId);

            entity.HasOne(d => d.Variant).WithMany(p => p.PvAggregatorstbls).HasForeignKey(d => d.VariantId);

            entity.HasOne(d => d.YearOfRegistrationNavigation).WithMany(p => p.PvAggregatorstbls).HasForeignKey(d => d.YearOfRegistration);
        });

        modelBuilder.Entity<PvNewCarDealerstbl>(entity =>
        {
            entity.ToTable("PV_NewCarDealerstbl");

            entity.HasIndex(e => e.UserInfoId, "IX_PV_NewCarDealerstbl_UserInfoId");

            entity.Property(e => e.PictOfOrginalRc).HasColumnName("PictOfOrginalRC");
        });

        modelBuilder.Entity<PvOpenMarketstbl>(entity =>
        {
            entity.ToTable("PV_OpenMarketstbl");

            entity.HasIndex(e => e.UserInfoId, "IX_PV_OpenMarketstbl_UserInfoId");

            entity.Property(e => e.PictureOfOriginalRc).HasColumnName("PictureOfOriginalRC");
            entity.Property(e => e.SellerContactNumber).HasMaxLength(12);
            entity.Property(e => e.SellerPan).HasColumnName("SellerPAN");
        });

        modelBuilder.Entity<PvaMaketbl>(entity =>
        {
            entity.HasKey(e => e.MakeId);

            entity.ToTable("PVA_Maketbl");
        });

        modelBuilder.Entity<PvaModeltbl>(entity =>
        {
            entity.HasKey(e => e.ModelId);

            entity.ToTable("PVA_Modeltbl");
        });

        modelBuilder.Entity<PvaVarianttbl>(entity =>
        {
            entity.HasKey(e => e.VariantId);

            entity.ToTable("PVA_Varianttbl");
        });

        modelBuilder.Entity<PvaYearOfRegtbl>(entity =>
        {
            entity.HasKey(e => e.YearId);

            entity.ToTable("PVA_YearOfRegtbl");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
