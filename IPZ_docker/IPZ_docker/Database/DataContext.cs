using IPZ_docker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IPZ_docker.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
                .HasOne(c => c.Purchase)
                .WithOne(p => p.Car)
                .HasForeignKey<Purchase>(p => p.CarId)
                .IsRequired();

            builder.Entity<Purchase>()
                .HasOne(p => p.Car)
                .WithOne(c => c.Purchase)
                .HasForeignKey<Car>(c => c.PurchaseId);

            builder.Entity<Client>()
                .HasMany(c => c.Purchases)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            builder.Entity<Client>()
                .HasData(
                    new Client
                    {
                        Id = 1,
                        Name = "Smyslov Danil",
                        Age = 20,
                        Sex = "Male"
                    }
                );

            builder.Entity<Car>()
                .HasData(
                    new Car
                    {
                        Id = 1,
                        CarType = "Daewoo Lanos 2007",
                        Price = 1950,
                        Mileage = 250000,
                        CarStatus = "Perfect",
                        PurchaseId = 1
                    }
                );
            builder.Entity<Purchase>()
                .HasData(
                    new Purchase
                    {
                        Id = 1,
                        ClientId = 1,
                        CarId = 1
                    }
                );

            base.OnModelCreating(builder);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new DataContext(builder.Options);
        }
    }
}
