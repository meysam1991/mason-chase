using Mc2.CrudTest.ModelFramework.Configuration;
using Mc2.CrudTest.ModelFramework.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.ModelFramework.StartupExtensions
{
    public static class SirvanTspFrameworkServicesExtensions
    {
        public static IServiceCollection AddSirvanTspFrameworkServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var sirvanConfigurations = new Mc2CrudTestFrameworkConfiguration();
            configuration.GetSection(nameof(Mc2CrudTestFrameworkConfiguration)).Bind(sirvanConfigurations);
            services.AddSingleton(sirvanConfigurations);

            var sirvanConfiguration = services.BuildServiceProvider().GetService<Mc2CrudTestFrameworkConfiguration>();

            services.AddScoped<ValidateModelStateAttribute>();

            services.AddControllers(options =>
            {
                if (sirvanConfiguration?.TrackActionPerformanceEnabled ?? false)
                    options.Filters.Add<TrackActionPerformanceFilter>();

                options.Filters.AddService<ValidateModelStateAttribute>();
            });

            services.AddSirvanTspFrameworkDependencies(sirvanConfiguration.AssemblyNamesForLoad);
           

            return services;
        }
    }
}