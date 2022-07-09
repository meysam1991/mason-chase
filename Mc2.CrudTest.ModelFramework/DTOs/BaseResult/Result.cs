using System.Collections.Generic;
using FluentValidation.Results;

namespace Mc2.CrudTest.ModelFramework.DTOs.BaseResult
{
    public class Result<TData> : BaseResult
    {
        public Result()
        {

        }
        public Result(Error error) : base(error)
        {
        }
        public Result(TData data)
        {
            Success = true;
            Data = data;
            ValidationErrors = null;
        }

        public Result(ValidationFailure error) : base(error)
        {
        }

        public Result(IEnumerable<ValidationFailure> errors) : base(errors)
        {
        }

        

        public Result(IEnumerable<Error> errors) : base(errors)
        {
        }
        public BaseResult AsBaseResult()
        {
            if (Success)
                return new BaseResult(SuccessMessage);

            return new BaseResult(ValidationErrors, OperationErrors);
        }

        public TData Data { get; protected set; }
    }
}