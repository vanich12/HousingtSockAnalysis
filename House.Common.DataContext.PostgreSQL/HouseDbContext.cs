using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace House.Common.EntityModels.PostgreSQL.Packt.Shared;

public partial class HouseDbContext : DbContext
{
    public HouseDbContext()
    {
    }

    public HouseDbContext(DbContextOptions<HouseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddToFavorite> AddToFavorites { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<HistoryOfChange> HistoryOfChanges { get; set; }

    public virtual DbSet<LivingComplex> LivingComplexes { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<TypeofHousing> TypeofHousings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HouseStockAnalysis;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddToFavorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AddToFavorites_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OfferId)
                .ValueGeneratedOnAdd()
                .HasColumnName("offerId");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("userId");

            entity.HasOne(d => d.Offer).WithMany(p => p.AddToFavorites)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AddToFavorites_offerId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AddToFavorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AddToFavorites_userId_fkey");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Address_pkey");

            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.DistrictId).ValueGeneratedOnAdd();
            entity.Property(e => e.HouseNumber).HasMaxLength(20);
            entity.Property(e => e.Street).HasMaxLength(50);

            entity.HasOne(d => d.District).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Address_DistrictId_fkey");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("District_pkey");

            entity.ToTable("District");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<HistoryOfChange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HistoryOfChanges_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OfferId)
                .ValueGeneratedOnAdd()
                .HasColumnName("offerId");

            entity.HasOne(d => d.Offer).WithMany(p => p.HistoryOfChanges)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HistoryOfChanges_offerId_fkey");
        });

        modelBuilder.Entity<LivingComplex>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LivingComplex_pkey");

            entity.ToTable("LivingComplex");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FullUrl)
                .HasColumnType("character varying")
                .HasColumnName("fullUrl");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Offer_pkey");

            entity.ToTable("Offer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).ValueGeneratedOnAdd();
            entity.Property(e => e.Jk)
                .ValueGeneratedOnAdd()
                .HasColumnName("jk");
            entity.Property(e => e.Photos).HasColumnType("jsonb");
            entity.Property(e => e.TypeofHousing).HasMaxLength(30);
            entity.Property(e => e.YuarBuilt).HasColumnName("Yuar_built");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .HasColumnName("Zip_Code");

            entity.HasOne(d => d.Address).WithMany(p => p.Offers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Offer_AddressId_fkey");

            entity.HasOne(d => d.JkNavigation).WithMany(p => p.Offers)
                .HasForeignKey(d => d.Jk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Offer_jk_fkey");

            entity.HasOne(d => d.TypeofHousingNavigation).WithMany(p => p.Offers)
                .HasForeignKey(d => d.TypeofHousing)
                .HasConstraintName("Offer_TypeofHousing_fkey");
        });

        modelBuilder.Entity<TypeofHousing>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("TypeofHousing_pkey");

            entity.ToTable("TypeofHousing");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
