using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Events;
using Mc2.CrudTest.ModelFramework.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.ModelFramework.StartupExtensions
{
    public static class ApplicationServicesDependenciesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            Assembly[] assemblies) => services
            .AddCommandHandlers(assemblies)
            .AddCommandDispatcherDecorators()
            .AddQueryHandlers(assemblies)
            .AddEventHandlers(assemblies)
            .AddFluentValidators(assemblies);

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandHandler<>),
                typeof(ICommandHandler<,>), typeof(ICommandHandler<,,>));

        private static IServiceCollection AddCommandDispatcherDecorators(this IServiceCollection services)
        {
            services.AddTransient<CommandDispatcher, CommandDispatcher>();
            services.AddTransient<CommandDispatcherDomainExceptionHandlerDecorator, CommandDispatcherDomainExceptionHandlerDecorator>();
            services.AddTransient<CommandDispatcherValidationDecorator, CommandDispatcherValidationDecorator>();
            services.AddTransient<ICommandDispatcher, CommandDispatcherValidationDecorator>();
            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IQueryHandler<,,>), typeof(IQueryHandler<,>),
                typeof(IQueryDispatcher));

        private static IServiceCollection AddEventHandlers(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IDomainEventHandler<>),
                typeof(IEventDispatcher));

        private static IServiceCollection AddFluentValidators(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddValidatorsFromAssemblies(assembliesForSearch);
    }
}