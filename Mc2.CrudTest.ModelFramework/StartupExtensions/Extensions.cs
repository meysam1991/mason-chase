using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mc2.CrudTest.ModelFramework.Services.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Mc2.CrudTest.ModelFramework.StartupExtensions
{
    public static class Extensions
    {
        public static IServiceCollection AddSirvanTspFrameworkDependencies(this IServiceCollection services,
            params string[] assemblyNamesForSearch)
        {
            var assemblies = GetAssemblies(assemblyNamesForSearch);
            services.AddApplicationServices(assemblies).AddDataAccess(assemblies)
                .AddSirvanTspFrameworkServicesDependencies(assemblies).AddCustomDependencies(assemblies);
            return services;
        }

        public static IServiceCollection AddCustomDependencies(this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            return services.AddWithTransientLifetime(assemblies, typeof(ITransientLifetime))
                .AddWithScopedLifetime(assemblies, typeof(IScopeLifetime))
                .AddWithSingletonLifetime(assemblies, typeof(ISingletonLifetime));
        }

        public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return services;
        }

        public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }

        public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }

        private static Assembly[] GetAssemblies(string[] assemblyNames)
        {
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            var loadMeLibraries = dependencies.Where(x => IsCandidateCompilationLibrary(x, assemblyNames));

            return loadMeLibraries.Select(loadMeLibrary => Assembly.Load(new AssemblyName(loadMeLibrary.Name)))
                .ToArray();
        }

        private static bool IsCandidateCompilationLibrary(Library compilationLibrary, string[] assemblyName)
        {
            return assemblyName.Any(d => compilationLibrary.Name.Contains(d))
                   || compilationLibrary.Dependencies.Any(d => assemblyName.Any(c => d.Name.Contains(c)));
        }
    }
}