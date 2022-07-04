using FluentValidation;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;


namespace Mc2.CrudTest.ModelFramework.Command
{
    public class CommandDispatcherValidationDecorator : CommandDispatcherDecorator
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcherValidationDecorator(CommandDispatcherDomainExceptionHandlerDecorator commandDispatcher
            , IServiceProvider serviceProvider) : base(commandDispatcher)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task<TResult> SendAsync<TCommand, TResult>(TCommand command)
        {
            var validationResult = Validate<TCommand, TResult>(command);
            return validationResult == null
                ? CommandDispatcher.SendAsync<TCommand, TResult>(command)
                : Task.FromResult(validationResult);
        }

        public override Task<BaseResult> SendAsync<TCommand>(TCommand command)
        {
            var validationResult = Validate<TCommand, BaseResult>(command);
            return validationResult != null ? Task.FromResult(validationResult) : CommandDispatcher.SendAsync(command);
        }

        public override Task<TResultContainer> SendAsync<TCommand, TResultContainer, TResultData>(TCommand command)
        {
            var validationResult = Validate<TCommand, TResultContainer>(command);
            return validationResult != null
                ? Task.FromResult(validationResult)
                : CommandDispatcher.SendAsync<TCommand, TResultContainer, TResultData>(command);
        }

        private TValidationResult Validate<TCommand, TValidationResult>(TCommand command)
            where TValidationResult : BaseResult, new()
        {
            var validator = _serviceProvider.GetService<IValidator<TCommand>>();
            if (validator == null) return null;
            var validationResult = validator.Validate(command);
            if (validationResult.IsValid) return null;
            var res = new TValidationResult();
            res.AddValidationError(validationResult.Errors);
            return res;
        }
    }
}