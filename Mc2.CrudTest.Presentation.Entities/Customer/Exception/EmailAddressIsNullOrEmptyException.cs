﻿
using Mc2.CrudTest.ModelFramework.Exceptions;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.DomainModel.Customer.Exception
{
    public class EmailAddressIsNullOrEmptyException : DomainStateException
    {
        public EmailAddressIsNullOrEmptyException(params InputParameter[] inputParameter) : base(ErrorCode.MandatoryField,
            "Email Address Not Entered",
            inputParameter)
        {
        }
    }
}