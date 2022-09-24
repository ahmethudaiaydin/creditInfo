using CreditInfo.Domain;
using Microsoft.EntityFrameworkCore;

namespace CreditInfo.Data
{
    public class ContractContext : DbContext
    {
        public DbSet<ContractProcess> ContractProcesses { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Individual> Individuals { get; set; }

        public DbSet<SubjectRole> SubjectRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }
    }
}