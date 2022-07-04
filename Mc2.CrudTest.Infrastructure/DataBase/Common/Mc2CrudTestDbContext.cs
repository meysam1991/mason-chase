using Mc2.CrudTest.DomainModel.Customer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure.DataBase.Common
{
    public class Mc2CrudTestDbContext : DbContext
    {
        public Mc2CrudTestDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddAuditableShadowProperties();
            modelBuilder.AddDeleteAbleShadowProperties();
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}