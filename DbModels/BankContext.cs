using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbModels
{
    public class BankContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BankContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<ClientDB> Clients => Set<ClientDB>();
        public DbSet<EmployeeDB> Employees => Set<EmployeeDB>();
        public DbSet<AccountDB> Account => Set<AccountDB>();
        public BankContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
