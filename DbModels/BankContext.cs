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
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }

        public DbSet<ClientDB> Clients => Set<ClientDB>();
        public DbSet<EmployeeDB>  Employees => Set<EmployeeDB>();
        public DbSet<AccountDB> Account => Set<AccountDB>();
    }
}
