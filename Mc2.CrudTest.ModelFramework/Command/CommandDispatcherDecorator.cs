using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ModelFramework.Command
{
    public abstract class CommandDispatcherDecorator : ICommandDispatcher
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        protected CommandDispatcherDecorator(ICommandDispatcher commandDispatcher) => CommandDispatcher = commandDispatcher;
        public abstract Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class, ICommand
            where TResult : BaseResult, new();

        public abstract Task<BaseResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;

        public abstract Task<TResultContainer> SendAsync<TCommand, TResultContainer, TResultData>(TCommand command)
            where TCommand : class, ICommand<TResultData>
            where TResultContainer : Result<TResultData>, new()
            where TResultData : class;
    }
}