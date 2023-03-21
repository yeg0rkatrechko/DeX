using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DbModels
{
    public class BankContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BankContext(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration) : base(optionsBuilder.Options)
        {
            _configuration = configuration;
        }
        public DbSet<ClientDB> Clients => Set<ClientDB>();
        public DbSet<EmployeeDB> Employees => Set<EmployeeDB>();
        public DbSet<AccountDB> Account => Set<AccountDB>();
        public BankContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("BankAPI"));
        }
    }
}
