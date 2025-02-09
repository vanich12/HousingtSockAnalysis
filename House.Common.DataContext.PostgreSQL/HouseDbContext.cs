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

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Liquidityresult> Liquidityresults { get; set; }

    public virtual DbSet<Marketdatum> Marketdata { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<User?> Users { get; set; }

    public virtual DbSet<Userinput> Userinputs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HouseDB;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Liquidityresult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("liquidityresults_pkey");

            entity.ToTable("liquidityresults");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.CalculationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("calculation_date");
            entity.Property(e => e.CashFlow)
                .HasPrecision(10, 2)
                .HasColumnName("cash_flow");
            entity.Property(e => e.InputId).HasColumnName("input_id");
            entity.Property(e => e.LiquidityScore)
                .HasPrecision(10, 2)
                .HasColumnName("liquidity_score");
            entity.Property(e => e.NetIncome)
                .HasPrecision(10, 2)
                .HasColumnName("net_income");
            entity.Property(e => e.Roi)
                .HasPrecision(5, 2)
                .HasColumnName("roi");

            entity.HasOne(d => d.Input).WithMany(p => p.Liquidityresults)
                .HasForeignKey(d => d.InputId)
                .HasConstraintName("liquidityresults_input_id_fkey");
        });

        modelBuilder.Entity<Marketdatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("marketdata_pkey");

            entity.ToTable("marketdata");

            entity.Property(e => e.Id).HasMaxLength(256);
            entity.Property(e => e.AverageRent)
                .HasPrecision(10, 2)
                .HasColumnName("average_rent");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.GrowthRate)
                .HasPrecision(5, 2)
                .HasColumnName("growth_rate");
            entity.Property(e => e.RentalDemand).HasColumnName("rental_demand");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.VacancyRate)
                .HasPrecision(5, 2)
                .HasColumnName("vacancy_rate");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("properties_pkey");

            entity.ToTable("properties");

            entity.Property(e => e.PropertyId).HasColumnName("property_id");
            entity.Property(e => e.AdditionalCosts)
                .HasPrecision(10, 2)
                .HasColumnName("additional_costs");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.AreaSqm)
                .HasPrecision(10, 2)
                .HasColumnName("area_sqm");
            entity.Property(e => e.Bathrooms).HasColumnName("bathrooms");
            entity.Property(e => e.Bedrooms).HasColumnName("bedrooms");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Insurance)
                .HasPrecision(10, 2)
                .HasColumnName("insurance");
            entity.Property(e => e.MaintenanceFees)
                .HasPrecision(10, 2)
                .HasColumnName("maintenance_fees");
            entity.Property(e => e.PricePerMonth)
                .HasPrecision(10, 2)
                .HasColumnName("price_per_month");
            entity.Property(e => e.PropertyType)
                .HasMaxLength(100)
                .HasColumnName("property_type");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.Taxes)
                .HasPrecision(10, 2)
                .HasColumnName("taxes");
            entity.Property(e => e.Utilities)
                .HasPrecision(10, 2)
                .HasColumnName("utilities");
            entity.Property(e => e.YearBuilt).HasColumnName("year_built");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.HasIndex(e => e.UserName, "UQ_Users_UserName").IsUnique();

            entity.Property(e => e.Id).HasMaxLength(256);
            entity.Property(e => e.AccessFailedCount).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EmailConfirmed).HasDefaultValue(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LockoutEnabled).HasDefaultValue(false);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.PhoneNumberConfirmed).HasDefaultValue(false);
            entity.Property(e => e.TwoFactorEnabled).HasDefaultValue(false);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<Userinput>(entity =>
        {
            entity.HasKey(e => e.InputId).HasName("userinput_pkey");

            entity.ToTable("userinput");

            entity.Property(e => e.InputId).HasColumnName("input_id");
            entity.Property(e => e.CalculationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("calculation_date");
            entity.Property(e => e.EstimatedExpenses)
                .HasPrecision(10, 2)
                .HasColumnName("estimated_expenses");
            entity.Property(e => e.EstimatedRent)
                .HasPrecision(10, 2)
                .HasColumnName("estimated_rent");
            entity.Property(e => e.ExpectedReturn)
                .HasPrecision(5, 2)
                .HasColumnName("expected_return");
            entity.Property(e => e.InvestmentAmount)
                .HasPrecision(10, 2)
                .HasColumnName("investment_amount");
            entity.Property(e => e.PropertyId).HasColumnName("property_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Property).WithMany(p => p.Userinputs)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("userinput_property_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
