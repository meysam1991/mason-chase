using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;


namespace Mc2.CrudTest.Shared.ValueObjects.BankAccountNumber
{
    public class BankAccountNumberIsEmptyOrNullException : DomainStateException
    {
        public BankAccountNumberIsEmptyOrNullException(params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "Empty Bank Account",
            inputParameter)
        {
        }
    }
}