using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;

namespace Mc2.CrudTest.Shared.ValueObjects.Email
{
    public class InvalidEmailLenghtValueException : DomainStateException
    {
        public InvalidEmailLenghtValueException(int lentgh, params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
           "Invalid Email Length",
            inputParameter)
        {
        }
    }
}