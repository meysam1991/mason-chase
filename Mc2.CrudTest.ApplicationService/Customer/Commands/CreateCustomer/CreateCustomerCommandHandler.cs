using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Translations;
using Mc2.CrudTest.Shared.ErrorMessages;
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
        private readonly DomainErrorMessages _errorMessages;
        private readonly ITranslator _translator;

        public CreateCustomerCommandHandler(ITranslator translator
            , DomainErrorMessages errorMessages
            , ILogger<CreateCustomerCommandHandler> logger
            , ICustomerCommandRepository repository
            , IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _translator = translator;
            _errorMessages = errorMessages;
        }

        public async Task<BaseResult> Handle(CreateCustomerCommand command)
        {
            if (command == null)
                return new BaseResult(new Error(ErrorCode.EmptyData,
                    _translator.GetString(_errorMessages.EmptyData), nameof(command)));

            //var newCustomer = new Customer(command.Title,
            //    command.Description, command.RolesId, command.ProfileIds);

            //await _repository.AddAsync(newCartable);

            try
            {
                var commit = await _unitOfWork.CommitAsync();
                if (commit < 1)
                    return new BaseResult(new Error(ErrorCode.DatabaseCommitNotAffected,
                        _translator.GetString(_errorMessages.DatabaseCommitNotAffected)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new BaseResult(new Error(ErrorCode.DatabaseCommitException,
                    _translator.GetString(_errorMessages.DatabaseCommitNotAffected)));
            }

            return new BaseResult();
        }
    }
}