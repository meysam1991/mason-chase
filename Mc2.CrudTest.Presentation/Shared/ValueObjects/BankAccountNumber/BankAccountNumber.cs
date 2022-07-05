using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.ValueObjects;
 
namespace Mc2.CrudTest.Shared.ValueObjects.BankAccountNumber
{
    public class BankAccountNumber : BaseValueObject<BankAccountNumber>
    {
        public const short FirstNameLength = 100;
        public string Value { get; private set; }

        public BankAccountNumber(string value)
        {
            Value = value;
            Validate();
        }

        public override bool ObjectIsEqual(BankAccountNumber otherObject)
        {
            if (Value == otherObject.Value)
                return true;

            return false;
        }

        public override int ObjectGetHashCode()
        {
            return base.GetHashCode();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Value))
                throw new BankAccountNumberIsEmptyOrNullException(new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });

            if (Value.Length > 100)
                throw new InvalidBankAccountNumberLenghtValueException(100, new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });
        }
    }
}