using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.ValueObjects;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;

namespace Mc2.CrudTest.Shared.ValueObjects.PhoneNumber
{
    public class PhoneNumber : BaseValueObject<PhoneNumber>
    {
        public const short FirstNameLength = 100;
        public string Value { get; private set; }

        public PhoneNumber(string value)
        {
            Value = value;
            Validate();
        }

        public override bool ObjectIsEqual(PhoneNumber otherObject)
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
                throw new PhoneNumberIsEmptyOrNullException(new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });

            if (Value.Length > 100)
                throw new InvalidPhoneNumberLenghtValueException(100, new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });
        }
    }
}