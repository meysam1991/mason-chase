using FluentValidation;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        [System.Obsolete]
        public DeleteCustomerCommandValidator()
        {
            RuleFor(c => c.CustomerId)
          .NotEqual(0).NotNull().WithMessage("Invalid customerId");


        }
    }
}