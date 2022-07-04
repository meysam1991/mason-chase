﻿using FluentValidation;
using Mc2.CrudTest.ModelFramework.Translations;
using Mc2.CrudTest.Shared.ErrorMessages;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;

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

            RuleFor(x => x.LastName)
               .NotEmpty().WithMessage(translator.GetString(errorMessages.LastNameIsEmputyOrNullOnCreateCustomer))
               .MaximumLength(FirstName.FirstNameLength)
               .WithMessage(translator.GetString(errorMessages.InvalidMaxLengthOfLastNameOnCreateCustomer));

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage(translator.GetString(errorMessages.EmailIsEmputyOrNullOnCreateCustomer))
            .MaximumLength(FirstName.FirstNameLength)
            .WithMessage(translator.GetString(errorMessages.InvalidMaxLengthOfEmailOnCreateCustomer));
        }
    }
}