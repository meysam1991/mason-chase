using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;

namespace Mc2.CrudTest.Shared.ValueObjects.FirstName
{
    public class InvalidFirstNameLenghtValueException : DomainStateException
    {
        public InvalidFirstNameLenghtValueException(int lentgh, params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "Invalid FirstName Length",
            inputParameter)
        {
        }
    }
}