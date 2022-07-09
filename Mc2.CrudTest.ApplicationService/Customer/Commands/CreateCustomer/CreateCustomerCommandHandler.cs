using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ILogger _logger;
        private readonly ICustomerCommandRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(
             
               ILogger<CreateCustomerCommandHandler> logger
            , ICustomerCommandRepository repository
            , IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(CreateCustomerCommand command)
        {
            if (command == null)
                return new BaseResult(new Error(ErrorCode.EmptyData,
                    "Ivalid input", nameof(command)));

            var newCustomer = new DomainModel.Customer.Entities.Customer(command.FirstName, command.LastName, command.DateOfBirth
                , command.PhoneNumber, command.Email, command.BankAccountNumber);

            await _repository.AddAsync(newCustomer);

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