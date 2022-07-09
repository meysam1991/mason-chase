using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
namespace Mc2.CrudTest.DomainModel.Customer.Exception
{
    public class InvalidDateOfBirthException : DomainStateException
    {
        public InvalidDateOfBirthException(params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
           "Invalid DateOfBirthDate",
            inputParameter)
        {
        }
    }
}