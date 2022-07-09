using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
    {
        private readonly ILogger _logger;
        private readonly ICustomerCommandRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(
              ILogger<UpdateCustomerCommandHandler> logger
            , ICustomerCommandRepository repository
            , IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(UpdateCustomerCommand command)
        {
            if (command == null)
                return new BaseResult(new Error(ErrorCode.EmptyData,
                    "Ivalid input", nameof(command)));

            var exitCustomner =await _repository.FindAsync(command.CustomerId);
            if(exitCustomner == null)

                return new BaseResult(new Error(ErrorCode.NotFound,
                         "Customer Not Found", nameof(command)));

            exitCustomner.Update(command.FirstName, command.LastName, command.DateOfBirth, command.Email, command.BankAccountNumber,command.PhoneNumber);

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