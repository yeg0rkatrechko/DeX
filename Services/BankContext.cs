using DbModels;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BankContext : DbContext
    {
        public DbSet<ClientDB> Clients => Set<ClientDB>();
        public DbSet<EmployeeDB>  Employees => Set<EmployeeDB>();
        public DbSet<AccountDB> Account => Set<AccountDB>();
        public DbSet<CurrencyDB> Currencies { get; set; }
        public BankContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-GFDH238\SQLEXPRESS;Database=bankdb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
