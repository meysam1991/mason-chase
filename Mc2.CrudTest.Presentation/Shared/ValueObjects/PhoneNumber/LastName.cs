using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.ValueObjects;
using Mc2.CrudTest.Shared.ValueObjects.FirstName;

namespace Mc2.CrudTest.Shared.ValueObjects.LastName
{
    public class LastName : BaseValueObject<LastName>
    {
        public const short FirstNameLength = 100;
        public string Value { get; private set; }

        public LastName(string value)
        {
            Value = value;
            Validate();
        }

        public override bool ObjectIsEqual(LastName otherObject)
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
                throw new LastNameIsEmptyOrNullException(new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });

            if (Value.Length > 100)
                throw new InvalidLastNameLenghtValueException(100, new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });
        }
    }
}