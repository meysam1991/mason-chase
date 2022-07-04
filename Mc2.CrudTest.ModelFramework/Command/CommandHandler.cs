using System;
using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Command
{
    public abstract class CommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
        where TCommand : class, ICommand<TData>, ICommand where TData : BaseResult
    {

        public Task<TData> Handle(TCommand command)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {

        public Task<BaseResult> Handle(TCommand command)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseCommandHandler<TCommand, TData> : ICommandHandler<TCommand, TData>
        where TCommand : class, ICommand<TData>, ICommand where TData : BaseResult
    {

        protected readonly CommandResult<TData> result = new CommandResult<TData>();
        protected virtual Task<CommandResult<TData>> OkAsync(TData data)
        {
            result._data = data;
            result.Status = ApplicationServiceStatus.Ok;
            return Task.FromResult(result);
        }

        public Task<TData> Handle(TCommand command)
        {
            throw new NotImplementedException();
        }
    }
}