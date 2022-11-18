using FluentValidation;
using FluentValidation.Validators;
using Mc2.CrudTest.Shared.ValidatorExtensions;

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

            RuleFor(x => x.PhoneNumber.IsValidMobileNumberLib("IR"))
                .NotEqual(false)
                .WithMessage("Invalid Mobile Number");



            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("BankAccountNumber is emputy or null")
                .MaximumLength(25)
                .WithMessage("Invalix max length of BankAccountNumber");
           

        }
       
    }
}