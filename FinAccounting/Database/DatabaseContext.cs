using Microsoft.EntityFrameworkCore;

namespace FinAccounting.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Receipt> receipt => Set<Receipt>();
        public DbSet<ReceiptPosition> receipt_position => Set<ReceiptPosition>();
        public DbSet<Product> product => Set<Product>();
        public DbSet<Shop> shop => Set<Shop>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={AppSettings.DBSettings.Host};Port={AppSettings.DBSettings.Port};Database={AppSettings.DBSettings.Database};Username={AppSettings.DBSettings.Username};Password={AppSettings.DBSettings.Password}");
            optionsBuilder.LogTo(System.Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(AppSettings.DBSettings.Schema);
            base.OnModelCreating(builder);
        }
    }
}
