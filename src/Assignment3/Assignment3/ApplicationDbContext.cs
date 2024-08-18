using Assignment3.TestCase1;
using Assignment3.TestCase2;
using Microsoft.EntityFrameworkCore;

namespace Assignment3
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext()
        {
            _connectionString = "Server= .\\SQLEXPRESS;Database=AspnetB9;User Id=aspnetb9;Password=123456;TrustServerCertificate=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Item> Item { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<TestClass1> TestClass1 { get; set; }
        public DbSet<TestClass2> TestClass2 { get; set; }

    }
}