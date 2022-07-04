using System.Collections.Generic;
using FluentValidation.Results;

namespace Mc2.CrudTest.ModelFramework.DTOs.BaseResult
{
    public class PagedResult<T> : BaseResult where T : class
    {
        public PagedResult(IList<T> data, int pageNumber, int totalRecordCount, int filteredRecordCount)
        {
            Success = true;
            ValidationErrors = null;
            Result = data;
            PageNumber = pageNumber;
            TotalRecordCount = totalRecordCount;
            FilteredRecordCount = filteredRecordCount;
            CurrentPageRecordCount = data?.Count ?? 0;
        }

        public PagedResult(Error error) : base(error)
        {
            Result = null;
        }

        public PagedResult(IEnumerable<Error> errors) : base(errors)
        {
            Result = null;
        }

        public PagedResult(IEnumerable<ValidationFailure> errors) : base(errors)
        {
            PageNumber = 0;
            TotalRecordCount = 0;
            FilteredRecordCount = 0;
            CurrentPageRecordCount = 0;
        }

        public PagedResult(ValidationFailure error) : base(error)
        {
            PageNumber = 0;
            TotalRecordCount = 0;
            FilteredRecordCount = 0;
            CurrentPageRecordCount = 0;
        }

        public int PageNumber { get; protected set; }
        public long TotalRecordCount { get; set; }
        public long FilteredRecordCount { get; set; }
        public long CurrentPageRecordCount { get; set; }
        public IEnumerable<T> Result { get; protected set; }
    }
}