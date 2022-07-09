using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;


namespace Mc2.CrudTest.Shared.ValueObjects.BankAccountNumber
{
    public class InvalidBankAccountNumberLenghtValueException : DomainStateException
    {
        public InvalidBankAccountNumberLenghtValueException(int lentgh, params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "Invalid BankAccount Lenght",
            inputParameter)
        {
        }
    }
}