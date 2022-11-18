using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.ValueObjects;
using System.Net;
using System;
using PhoneNumbers;
using Mc2.CrudTest.Shared.ValidatorExtensions;

namespace Mc2.CrudTest.Shared.ValueObjects.PhoneNumber
{
    public class PhoneNumber : BaseValueObject<PhoneNumber>
    {
        public const short FirstNameLength = 100;
        public const   string countryCode = "IR"; 
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

          
            if (!Value.IsValidMobileNumberLib(countryCode))
                throw new InvalidPhoneNumberValueException( new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });


            if (string.IsNullOrEmpty(Value))
                throw new PhoneNumberIsEmptyOrNullException(new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });

            if (Value.Length > 16)
                throw new InvalidPhoneNumberLenghtValueException(16, new InputParameter()
                { AttemptedValue = Value, PropertyName = nameof(Value) });
            Value = Value.OrginalMobileFormat(countryCode);
        }
    }
}