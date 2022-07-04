﻿namespace Mc2.CrudTest.Shared.ErrorMessages
{
    public class DomainErrorMessages
    {
        public string InvalidMaxLengthOfFirstNameOnCreateCustomer => nameof(InvalidMaxLengthOfFirstNameOnCreateCustomer);
        public string FirstNameIsEmputyOrNullOnCreateCustomer => nameof(FirstNameIsEmputyOrNullOnCreateCustomer);
        public string NotFoundPersonWithId => nameof(NotFoundPersonWithId);
        public string EmptyData => nameof(EmptyData);
        public string DatabaseCommitNotAffected => nameof(DatabaseCommitNotAffected);
        public string LastNameIsEmputyOrNullOnCreateCustomer => nameof(LastNameIsEmputyOrNullOnCreateCustomer);
        public string InvalidMaxLengthOfLastNameOnCreateCustomer => nameof(InvalidMaxLengthOfLastNameOnCreateCustomer);
        public string EmailIsEmputyOrNullOnCreateCustomer => nameof(EmailIsEmputyOrNullOnCreateCustomer);
        public string InvalidMaxLengthOfEmailOnCreateCustomer => nameof(InvalidMaxLengthOfEmailOnCreateCustomer);
    }
}