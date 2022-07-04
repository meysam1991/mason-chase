using FluentValidation;
using Mc2.CrudTest.ModelFramework.Translations;
using Mc2.CrudTest.Shared.ErrorMessages;
using Mc2.CrudTest.Shared.ValueObjects.Email;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;
using Mc2.CrudTest.Shared.ValueObjects.LastName;
using Mc2.CrudTest.Shared.ValueObjects.PhoneNumber;

namespace Mc2.CrudTest.ApplicationService.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator(ITranslator translator, DomainErrorMessages errorMessages)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(translator.GetString(errorMessages.FirstNameIsEmputyOrNullOnCreateCustomer))
                .MaximumLength(FirstName.FirstNameLength)
                .WithMessage(translator.GetString(errorMessages.InvalidMaxLengthOfFirstNameOnCreateCustomer));

            
        }
    }
}