
using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.ValueObjects;

namespace Mc2.CrudTest.Shared.ValueObjects.FirstName
{
    public class FirstName : BaseValueObject<FirstName>
    {
        public const short FirstNameLength = 100;
        public string Value { get; private set; }

        public FirstName(string value)
        {
            Value = value;
            Validate();
        }

        public override bool ObjectIsEqual(FirstName otherObject)
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
                throw new FirstNameIsEmptyOrNullException(new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });

            if (Value.Length > 100)
                throw new InvalidFirstNameLenghtValueException(100, new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });
        }
    }
}