using FinAccountingWebService.Database.Tables;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FinAccountingWebService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Receipt> Receipt => Set<Receipt>();
        public DbSet<ReceiptPosition> ReceiptPosition => Set<ReceiptPosition>();
        public DbSet<Product> Product => Set<Product>();
        public DbSet<Shop> Shop => Set<Shop>();

        public DatabaseContext(bool recreateBase = false)
        {
            if (recreateBase) 
                Database.EnsureDeleted();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={AppSettings.DBSettings.Host};Port={AppSettings.DBSettings.Port};Database={AppSettings.DBSettings.Database};Username={AppSettings.DBSettings.Username};Password={AppSettings.DBSettings.Password}");
            optionsBuilder.LogTo(System.Console.WriteLine, LogLevel.Warning);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(AppSettings.DBSettings.Schema);

            builder.Entity<Product>().HasAlternateKey(p => p.Title);
            builder.Entity<Shop>().HasAlternateKey(s => s.Title);

            builder.Entity<Receipt>()
                   .HasOne(r => r.Shop)
                   .WithMany(s => s.Receipts)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Receipt>()
                   .HasMany(r => r.Products)
                   .WithMany(p => p.Receipts)
                   .UsingEntity<ReceiptPosition>(
                    i => i
                    .HasOne(rp => rp.Product)
                    .WithMany(p => p.ReceiptPositions)
                    .HasForeignKey(rp => rp.ProductId)
                    .OnDelete(DeleteBehavior.NoAction),
                    i => i
                    .HasOne(rp => rp.Receipt)
                    .WithMany(r => r.ReceiptPositions)
                    .HasForeignKey(rp => rp.ReceiptId)
                    .OnDelete(DeleteBehavior.NoAction),
                    i =>
                    {
                        i.Property(rp => rp.Price).HasDefaultValue(0m);
                        i.Property(rp => rp.Count).HasDefaultValue(0m);
                        i.HasKey(rp => new { rp.ReceiptId, rp.ProductId });
                    });
        }
    }
}
