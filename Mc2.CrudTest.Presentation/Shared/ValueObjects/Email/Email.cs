using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.ValueObjects;

namespace Mc2.CrudTest.Shared.ValueObjects.Email
{
    public class Email : BaseValueObject<Email>
    {
        public const short FirstNameLength = 100;
        public string Value { get; private set; }

        public Email(string value)
        {
            Value = value.Normalize();
            Validate();
        }

        public override bool ObjectIsEqual(Email otherObject)
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
                throw new EmailIsEmptyOrNullException(new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });

            if (Value.Length > 100)
                throw new InvalidEmailLenghtValueException(100, new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });
        }
    }
}