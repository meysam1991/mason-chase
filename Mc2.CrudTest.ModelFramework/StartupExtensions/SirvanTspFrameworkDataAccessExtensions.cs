using System.Collections.Generic;
using System.Reflection;
using Mc2.CrudTest.ModelFramework.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mc2.CrudTest.ModelFramework.StartupExtensions
{
    public static class SirvanTspFrameworkDataAccessExtensions
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            Assembly[] assembliesForSearch) =>
            services.AddRepositories(assembliesForSearch)
                .AddUnitOfWorks(assembliesForSearch)
                .AddApiRepositories(assembliesForSearch);

        public static IServiceCollection AddRepositories(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
             
            return services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandRepository<,>),
                typeof(IQueryRepository));
        }

        public static IServiceCollection AddApiRepositories(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IApiRepository));

        public static IServiceCollection AddUnitOfWorks(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));
            return services;
        }

        
         
    }
}