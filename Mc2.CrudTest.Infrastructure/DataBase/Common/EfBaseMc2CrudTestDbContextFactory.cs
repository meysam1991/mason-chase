using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mc2.CrudTest.Infrastructure.DataBase.Common
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<EfBaseMc2CrudTest>
    {
        public EfBaseMc2CrudTest CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder();

            var connectionString = configuration
                .GetConnectionString("SirvanTspNotification");

            optionsBuilder.UseSqlServer(connectionString);

            return new EfBaseMc2CrudTest(optionsBuilder.Options);
        }
    }
}
