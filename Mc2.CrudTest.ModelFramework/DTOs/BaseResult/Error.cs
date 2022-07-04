namespace Mc2.CrudTest.ModelFramework.DTOs.BaseResult
{
    public class Error
    {
        public Error(ErrorCode code, string description = null, string fieldName = null)
        {
            Code = code;
            Description = description;
            FieldName = fieldName;
        }

        public Error(ErrorCode errorCode, string message, string[] parameters)
        {
        }

        public ErrorCode Code { get; protected set; }
        public string FieldName { get; protected set; }
        public string Description { get; protected set; }
    }
}