using FluentValidation;
using Mc2.CrudTest.Shared.ErrorMessages;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        [System.Obsolete]
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is emputy or null")
                .MaximumLength(100)
                .WithMessage("Invalix max length of FirstName");

            RuleFor(x => x.LastName)
               .NotEmpty().WithMessage("LastName is emputy or null")
               .MaximumLength(100)
               .WithMessage("Invalix max length of LastName");

              RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is emputy or null")
            .MaximumLength(100)
            .WithMessage("Invalix max length of Email")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex).WithMessage("Invalid email");

            RuleFor(customer => customer.PhoneNumber).Matches("^09\\d{2}\\d{3}\\d{4}$").WithMessage("Invalid phone number");

            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("BankAccountNumber is emputy or null")
                .MaximumLength(25)
                .WithMessage("Invalix max length of BankAccountNumber");


        }
    }
}