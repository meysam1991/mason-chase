using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Mc2.CrudTest.ModelFramework.DTOs.BaseResult
{
    public class BaseResult
    {
        public BaseResult()
        {
            Success = true;
            ValidationErrors = null;
        }

        public BaseResult(string successMessage)
        {
            Success = true;
            SuccessMessage = successMessage;
            ValidationErrors = null;
        }

        public BaseResult(ValidationFailure error)
        {
            ValidationErrors ??= new List<ValidationFailure>();
            ValidationErrors.Add(error);
            Success = false;
            SuccessMessage = null;
        }

        public BaseResult(Error error)
        {
            OperationErrors ??= new List<Error>();
            OperationErrors.Add(error);
            Success = false;
            SuccessMessage = null;
        }

        public BaseResult(IEnumerable<Error> errors)
        {
            OperationErrors ??= new List<Error>();
            OperationErrors.AddRange(errors);
            Success = false;
            SuccessMessage = null;
        }

        public BaseResult(IEnumerable<ValidationFailure> errors)
        {
            ValidationErrors = errors.ToList();
            Success = false;
            SuccessMessage = null;
        }
        public BaseResult(IEnumerable<ValidationFailure> validationErrors, IEnumerable<Error> operationErrors)
        {
            ValidationErrors = validationErrors.ToList();
            OperationErrors = operationErrors.ToList();
            Success = false;
            SuccessMessage = null;
        }

        public void AddOperationError(Error error)
        {
            Success = false;
            SuccessMessage = null;
            OperationErrors ??= new List<Error>();
            OperationErrors.Add(error);
        }

        public void AddOperationError(IEnumerable<Error> errors)
        {
            Success = false;
            SuccessMessage = null;
            OperationErrors ??= new List<Error>();
            OperationErrors.AddRange(errors);
        }

        public void AddValidationError(ValidationFailure error)
        {
            Success = false;
            SuccessMessage = null;
            ValidationErrors ??= new List<ValidationFailure>();
            ValidationErrors.Add(error);
        }

        public void AddValidationError(IEnumerable<ValidationFailure> errors)
        {
            Success = false;
            SuccessMessage = null;
            ValidationErrors ??= new List<ValidationFailure>();
            ValidationErrors.AddRange(errors);
        }

        public bool Success { get; protected set; }
        public List<ValidationFailure> ValidationErrors { get; protected set; }
        public List<Error> OperationErrors { get; protected set; }
        public string SuccessMessage { get; protected set; }
    }
}