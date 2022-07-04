using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.Shared.ErrorMessages;

namespace Mc2.CrudTest.Shared.ValueObjects.LastName
{
    public class LastNameIsEmptyOrNullException : DomainStateException
    {
        public LastNameIsEmptyOrNullException(params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            DomainExceptionMessages.EmptyLastName,
            inputParameter)
        {
        }
    }
}