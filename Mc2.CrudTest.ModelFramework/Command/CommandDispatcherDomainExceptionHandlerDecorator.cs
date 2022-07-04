using System;
using System.Linq;
using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.Translations;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.ModelFramework.Command
{
    public class CommandDispatcherDomainExceptionHandlerDecorator : CommandDispatcherDecorator
    {
        private readonly ITranslator _translator;
        private readonly ILogger _logger;

        public CommandDispatcherDomainExceptionHandlerDecorator(
            CommandDispatcher commandDispatcher, ITranslator translator, ILogger<CommandDispatcherDomainExceptionHandlerDecorator> logger)
            : base(commandDispatcher)
        {
            _translator = translator;
            _logger = logger;
        }

        public override async Task<TResult> SendAsync<TCommand, TResult>(TCommand command)
        {
            try
            {
                return await CommandDispatcher.SendAsync<TCommand, TResult>(command);
            }
            catch (DomainStateException ex)
            {
                _logger.LogError(ex, ex.Message);
                return await DomainExceptionHandling<TResult>(ex);
            }
        }

        public override async Task<BaseResult> SendAsync<TCommand>(TCommand command)
        {
            try
            {
                return await CommandDispatcher.SendAsync(command);
            }
            catch (DomainStateException ex)
            {
                _logger.LogError(ex, ex.Message);
                return await DomainExceptionHandling(ex);
            }
        }

        public override async Task<TResultContainer> SendAsync<TCommand, TResultContainer, TResultData>(
            TCommand command)
        {
            try
            {
                return await CommandDispatcher.SendAsync<TCommand, TResultContainer, TResultData>(command);
            }
            catch (DomainStateException ex)
            {
                _logger.LogError(ex, ex.Message);
                return await DomainExceptionHandling<TResultContainer>(ex);
            }
        }

        private async Task<TCommandResult> DomainExceptionHandling<TCommandResult>(DomainStateException exception)
            where TCommandResult : BaseResult
        {
            var type = typeof(TCommandResult);
            dynamic commandResult = new BaseResult();
            if (type.IsGenericType)
            {
                var d1 = typeof(Result<>);
                var makeMe = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeMe);
            }

            var message = _translator.GetString(exception.Message);

            if (exception.InputParameters.Any())
                commandResult?.AddOperationError(exception.InputParameters.Select(x =>
                    new Error(exception.ErrorCode, message, x.PropertyName)).ToList());
            else
                commandResult?.AddOperationError(new Error(ErrorCode.InvalidOperation, message));

            return await Task.FromResult(commandResult as TCommandResult);
        }

        private async Task<BaseResult> DomainExceptionHandling(DomainStateException exception)
        {
            var type = typeof(BaseResult);
            dynamic commandResult = new BaseResult();
            if (type.IsGenericType)
            {
                var d1 = typeof(Result<>);
                var makeMe = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeMe);
            }

            var message = _translator.GetString(exception.Message);

            if (exception.InputParameters.Any())
                commandResult?.AddOperationError(exception.InputParameters.Select(x =>
                    new Error(exception.ErrorCode, message, x.PropertyName)).ToList());
            else
                commandResult?.AddOperationError(new Error(ErrorCode.InvalidOperation, message));

            return await Task.FromResult(commandResult as BaseResult);
        }
    }
}