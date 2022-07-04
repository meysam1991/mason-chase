using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Command
{
    public interface ICommandDispatcher
    {


        Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class, ICommand
            where TResult : BaseResult, new();

        Task<BaseResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;

        Task<TResultContainer> SendAsync<TCommand, TResultContainer, TResultData>(TCommand command)
            where TCommand : class, ICommand<TResultData>
            where TResultContainer : Result<TResultData>, new()
            where TResultData : class;



    }
}
