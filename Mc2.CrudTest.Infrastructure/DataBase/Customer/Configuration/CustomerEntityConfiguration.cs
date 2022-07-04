using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mc2.CrudTest.Infrastructure.DataBase.Customer.Configuration
{
    public class
        CustomerEntityConfiguration : IEntityTypeConfiguration<DomainModel.Customer.Entities.Customer>
    {
        [System.Obsolete]
        public void Configure(EntityTypeBuilder<DomainModel.Customer.Entities.Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //builder.OwnsOne(x => x.FirstName).Property(x => x.Value).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            builder.OwnsOne(x => x.LastName).Property(x => x.Value).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            builder.OwnsOne(x => x.PhoneNumber).Property(x => x.Value).HasColumnName("PhoneNumber").HasMaxLength(100).IsRequired();
            builder.OwnsOne(x => x.Email).Property(x => x.Value).HasColumnName("Email").HasMaxLength(100).IsRequired();
            builder.OwnsOne(x => x.BankAccountNumber).Property(x => x.Value).HasColumnName("BankAccountNumber").HasMaxLength(100).IsRequired();


             builder.HasIndex(x => new { FirstName = x.FirstName.Value, LastNAme = x.LastName.Value, x.DateOfBirth }).IsUnique();
             

        }
    }
}