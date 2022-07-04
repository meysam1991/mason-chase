using System;
using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Exceptions
{
    public class DomainStateException : Exception
    {
        public InputParameter[] InputParameters { get; set; }


        public ErrorCode ErrorCode { get; }
        public DomainStateException(string message, params InputParameter[] parameters) : base(message)
        {
            InputParameters = parameters;

        }

        public DomainStateException(ErrorCode errorCode, string message, params InputParameter[] parameters) : base(message)
        {
            InputParameters = parameters;
            ErrorCode = errorCode;
        }

        public string[] Parameters { get; set; }

        public DomainStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}