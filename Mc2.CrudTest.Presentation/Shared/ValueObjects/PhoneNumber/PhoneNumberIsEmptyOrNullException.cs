using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;

namespace Mc2.CrudTest.Shared.ValueObjects.PhoneNumber
{
    public class PhoneNumberIsEmptyOrNullException : DomainStateException
    {
        public PhoneNumberIsEmptyOrNullException(params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "Empty PhoneNumber",
            inputParameter)
        {
        }
    }
}