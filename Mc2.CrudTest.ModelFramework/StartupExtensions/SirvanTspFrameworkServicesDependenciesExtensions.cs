using System;
using System.Collections.Generic;
using System.Reflection;
using Mc2.CrudTest.ModelFramework.Configuration;
using Mc2.CrudTest.ModelFramework.Events;
using Mc2.CrudTest.ModelFramework.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.ModelFramework.StartupExtensions
{
    public static class SirvanTspFrameworkServicesDependenciesExtensions
    {
        public static IServiceCollection AddSirvanTspFrameworkServicesDependencies(this IServiceCollection services,
            Assembly[] assemblies)
        {
            services.AddLogging();
            services.AddPoolingPublisher(assemblies);
             
            return services;
        }

         


         

        private static IServiceCollection AddPoolingPublisher(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var sirvanTspFrameworkConfiguration =
                services.BuildServiceProvider().GetService<Mc2CrudTestFrameworkConfiguration>();
            if (sirvanTspFrameworkConfiguration.PoolingPublisher.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type =>
                        type.Name == sirvanTspFrameworkConfiguration.PoolingPublisher.OutBoxRepositoryTypeName &&
                        typeof(IOutBoxEventItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
                services.AddHostedService<PoolingPublisherHostedService>();
            }

            return services;
        }

        

        

         

        

        private static IServiceCollection AddLogging(this IServiceCollection services)
        {
            return services.AddScoped<IScopeInformation, ScopeInformation>();
        }

         

        
    }
}