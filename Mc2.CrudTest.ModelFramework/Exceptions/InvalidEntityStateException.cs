using Mc2.CrudTest.ModelFramework.DTOs.BaseResult;

namespace Mc2.CrudTest.ModelFramework.Exceptions
{
    public class InvalidEntityStateException : DomainStateException
    {
        public InvalidEntityStateException(string message, params InputParameter[] parameters) : base(message,
            parameters)
        {
        }

        public InvalidEntityStateException(ErrorCode errorCode, string message, params InputParameter[] parameters) :
            base(errorCode, message, parameters)
        {
        }
    }
}