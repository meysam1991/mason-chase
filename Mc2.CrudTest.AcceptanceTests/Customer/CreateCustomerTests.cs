using FluentAssertions;
using Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.DomainModel.Customer.Data;
using Mc2.CrudTest.ModelFramework.Command;
using Mc2.CrudTest.ModelFramework.Data;
using Mc2.CrudTest.ModelFramework.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Customer
{
    public class BddTddTests
    {
        private CreateCustomerCommandHandler _commandHandler;
        private readonly Mock<ILogger<CreateCustomerCommandHandler>> _logger;
        private readonly Mock<ICustomerCommandRepository> _repository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private CreateCustomerCommand _command;
        public BddTddTests()
        {
            Setup();
            _unitOfWork = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<CreateCustomerCommandHandler>>();
            _repository = new Mock<ICustomerCommandRepository>();
        }

        #region ImplementationTest

        [Fact]
        public void Should_Implement_ICommandHandler()
        {
            typeof(CreateCustomerCommandHandler).Should().Implement<ICommandHandler<CreateCustomerCommand>>();
        }

        [Fact]
        public void Should_Implement_ICommandRepository()
        {
            typeof(ICustomerCommandRepository).Should().Implement<ICommandRepository<DomainModel.Customer.Entities.Customer, int>>();
        }

        [Fact]
        public void Should_Implement_AddComplaintCommand()
        {
            typeof(CreateCustomerCommand).Should().Implement<ICommand>();

        }

        [Fact]
        public void Should_ComplaintSubject_Domain_DriveFrom_BaseAggregateRoot()
        {
            typeof(DomainModel.Customer.Entities.Customer).Should().BeDerivedFrom<BaseAggregateRoot<int>>();
        }
        #endregion

        #region BehavioralTest
        [Fact]
        public void ShouldNotBe_Null_CreateCustomerCommand()
        {
            //Arrange
            CreateCustomerCommand command = null;
            CreateCommandHandler();

            //Act
            var result = _commandHandler.Handle(command);

            //Assert
            Assert.False(result.Result.Success);
        }
         
        #endregion
        #region Setup

        private void Setup()
        {
            CreateCustomerCommandSetup("Meysam", "Mozaffari", "Meysam@me.com", "09182906145", "1234567891236548", DateTime.Now.ToString());
            CreateCommandHandler();
        }
        private void CreateCommandHandler()
        {
            _commandHandler = new CreateCustomerCommandHandler(_logger.Object, _repository.Object, _unitOfWork.Object);
        }
        private void CreateCustomerCommandSetup(string firstName, string lastName, string email, string phoneNumber, string bankAccount, string dateOfBirth)
        {
            _command = new CreateCustomerCommand()

            {
                BankAccountNumber = bankAccount,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth
            };

        }

        private void UnitOfWorkSetup(int commit)
        {

            _unitOfWork.Setup(s => s.CommitAsync().Result).Returns(commit);
        }
        #endregion
    }
}
