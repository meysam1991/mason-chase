using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.Shared.ErrorMessages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
    {
        private readonly ILogger _logger;
        private readonly ICustomerCommandRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainErrorMessages _errorMessages;

        public DeleteCustomerCommandHandler(
             DomainErrorMessages errorMessages
            , ILogger<DeleteCustomerCommandHandler> logger
            , ICustomerCommandRepository repository
            , IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _errorMessages = errorMessages;
        }

        public async Task<BaseResult> Handle(DeleteCustomerCommand command)
        {
            if (command == null)
                return new BaseResult(new Error(ErrorCode.EmptyData,
                    "Ivalid input", nameof(command)));

            var existCustomer = await _repository.FindAsync(command.CustomerId);
            if(existCustomer == null)
                return new BaseResult(new Error(ErrorCode.NotFound,
                   "Customer not found", nameof(command)));

            _repository.Delete(existCustomer);

            try
            {
                var commit = await _unitOfWork.CommitAsync();
                if (commit < 1)
                    return new BaseResult(new Error(ErrorCode.DatabaseCommitNotAffected,
                        "DatabaseCommitNotAffected"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new BaseResult(new Error(ErrorCode.DatabaseCommitException,
                   "DatabaseCommitNotAffected"));
            }

            return new BaseResult();
        }
    }
}