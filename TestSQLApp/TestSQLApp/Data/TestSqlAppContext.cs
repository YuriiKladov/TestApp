using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSQLApp.Models;

namespace TestSQLApp.Data
{
    public class TestSqlAppContext : DbContext
    {
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestSqlApp; Trusted_Connection=True");
        }
    }
}
