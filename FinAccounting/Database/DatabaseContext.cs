using Microsoft.EntityFrameworkCore;

namespace FinAccounting.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly string schema = "receipt"; // TODO: убрать в конфиг

        public DbSet<Receipt> receipt => Set<Receipt>();
        public DbSet<ReceiptPosition> receipt_position => Set<ReceiptPosition>();
        public DbSet<Product> product => Set<Product>();
        public DbSet<Shop> shop => Set<Shop>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=receipt;Username=postgres;Password=postgres"); // TODO: убрать в конфиг
            optionsBuilder.LogTo(System.Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(schema);
            base.OnModelCreating(builder);
        }
    }
}
