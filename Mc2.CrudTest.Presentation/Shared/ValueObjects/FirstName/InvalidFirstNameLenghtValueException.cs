﻿using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;
using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.Shared.ErrorMessages;

namespace Mc2.CrudTest.Shared.ValueObjects.FirstName
{
    public class InvalidFirstNameLenghtValueException : DomainStateException
    {
        public InvalidFirstNameLenghtValueException(int lentgh, params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            DomainExceptionMessages.InvalidLength,
            inputParameter)
        {
        }
    }
}