using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace Packt.Shared;

public partial class HouseDbContext : DbContext
{
    public HouseDbContext()
    {
    }

    public HouseDbContext(DbContextOptions<HouseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Liquidityresult> Liquidityresults { get; set; }

    public virtual DbSet<Marketdatum> Marketdata { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Userinput> Userinputs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HouseDB;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Liquidityresult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("liquidityresults_pkey");

            entity.Property(e => e.CalculationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Input).WithMany(p => p.Liquidityresults).HasConstraintName("liquidityresults_input_id_fkey");
        });

        modelBuilder.Entity<Marketdatum>(entity =>
        {
            entity.HasKey(e => e.MarketDataId).HasName("marketdata_pkey");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("properties_pkey");
        });

        modelBuilder.Entity<Userinput>(entity =>
        {
            entity.HasKey(e => e.InputId).HasName("userinput_pkey");

            entity.Property(e => e.CalculationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Property).WithMany(p => p.Userinputs).HasConstraintName("userinput_property_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
