using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Command
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : class, ICommand where TResult : BaseResult
    {
        Task<TResult> Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task<BaseResult> Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult, TData> where TCommand : ICommand<TData> where TResult : Result<TData>
    {
        Task<TResult> Handle(TCommand command);
    }


}