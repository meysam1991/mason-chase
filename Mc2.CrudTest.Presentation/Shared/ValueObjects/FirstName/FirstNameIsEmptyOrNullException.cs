using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;
namespace Mc2.CrudTest.Shared.ValueObjects.FirstName
{
    public class FirstNameIsEmptyOrNullException : DomainStateException
    {
        public FirstNameIsEmptyOrNullException(params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "Empty FirstName",
            inputParameter)
        {
        }
    }
}