namespace Mc2.CrudTest.ModelFramework.DTOs.BaseResult
{
    public enum ApplicationServiceStatus
    {
        Ok = 1,
        NotFound = 2,
        ValidationError = 3,
        InvalidDomainState = 4,
        Exception = 5,
        RecordAlreadyExist = 6
    }
}
