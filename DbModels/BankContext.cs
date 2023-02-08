using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    public class BankContext : DbContext
    {
        public DbSet<ClientDB> Clients => Set<ClientDB>();
        public DbSet<EmployeeDB>  Employees => Set<EmployeeDB>();
        public DbSet<AccountDB> Account => Set<AccountDB>();
        public BankContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-GFDH238\SQLEXPRESS;Database=bankdb;Trusted_Connection=True;");
        }
    }
}
