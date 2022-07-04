using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.ModelFramework.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class, ICommand
            where TResult : BaseResult, new()
        {
             
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await handler.Handle(command);
        }

        public async Task<BaseResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return await handler.Handle(command);
        }

        public async Task<TResultContainer> SendAsync<TCommand, TResultContainer, TResultData>(TCommand command)
            where TCommand : class, ICommand<TResultData>
            where TResultContainer : Result<TResultData>, new()
            where TResultData : class
        {
             
            var handler =
                _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResultContainer, TResultData>>();
            return await handler.Handle(command);
        }



        private TResult MakeInCorrectCaptchaResult<TResult>()
        {
            var type = typeof(TResult);
            dynamic commandResult = new BaseResult();
            if (type.IsGenericType)
            {
                var d1 = typeof(Result<>);
                var makeMe = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeMe);
            }

            commandResult?.AddValidationError(new ValidationFailure(
                "UserEnteredCaptchaCode", "کد کپچا صحیح نبوده است."));
            return commandResult;
        }

         

         
    }
}