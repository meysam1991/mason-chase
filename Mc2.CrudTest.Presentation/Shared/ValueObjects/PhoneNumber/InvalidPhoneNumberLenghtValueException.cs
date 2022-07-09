using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;

namespace Mc2.CrudTest.Shared.ValueObjects.PhoneNumber
{
    public class InvalidPhoneNumberLenghtValueException : DomainStateException
    {
        public InvalidPhoneNumberLenghtValueException(int lentgh, params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "InvalidPhoneNumberLenght",
            inputParameter)
        {
        }
    }
}