namespace Mc2.CrudTest.Shared.ErrorMessages
{
    public class DomainErrorMessages
    {
        

        public string InvalidMaxLengthOfFirstNameOnCreateCustomer => nameof(InvalidMaxLengthOfFirstNameOnCreateCustomer);
        public string FirstNameIsEmputyOrNullOnCreateCustomer => nameof(FirstNameIsEmputyOrNullOnCreateCustomer);
        public string NotFoundPersonWithId => nameof(NotFoundPersonWithId);
        public string EmptyData => nameof(EmptyData);
        public string DatabaseCommitNotAffected => nameof(DatabaseCommitNotAffected);
    }
}