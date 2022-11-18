using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;

namespace Mc2.CrudTest.Shared.ValueObjects.PhoneNumber
{
    public class InvalidPhoneNumberValueException : DomainStateException
    {
        public InvalidPhoneNumberValueException( params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "InvalidPhoneNumber",
            inputParameter)
        {
        }
    }
}