using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;
 

namespace Mc2.CrudTest.Shared.ValueObjects.Email
{
    public class EmailIsEmptyOrNullException : DomainStateException
    {
        public EmailIsEmptyOrNullException(params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "EmptyEmail",
            inputParameter)
        {
        }
    }
}