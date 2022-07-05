using Mc2.CrudTest.Shared.ValueObjects.BankAccountNumber;
using Mc2.CrudTest.Shared.ValueObjects.Email;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;
using Mc2.CrudTest.Shared.ValueObjects.LastName;
using Mc2.CrudTest.Shared.ValueObjects.PhoneNumber;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Infrastructure.DataBase.Customer.Configuration
{
    public class
        CustomerEntityConfiguration : IEntityTypeConfiguration<DomainModel.Customer.Entities.Customer>
    {

        public void Configure(EntityTypeBuilder<DomainModel.Customer.Entities.Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.FirstName).HasConversion(c => c.Value, c => new FirstName(c)).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasConversion(c => c.Value, c => new LastName(c)).HasMaxLength(150).IsRequired();
            builder.Property(c => c.PhoneNumber).HasConversion(c => c.Value, c => new PhoneNumber(c)).HasMaxLength(50);
            builder.Property(c => c.BankAccountNumber).HasConversion(c => c.Value, c => new BankAccountNumber(c)).HasMaxLength(16);
            builder.Property(c => c.Email).HasConversion(c => c.Value, c => new Email(c)).HasMaxLength(250);
            builder.HasIndex(x =>
            new {
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth
            }).IsUnique();


        }

    }
    }

