using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
        }

        // constuctor cho trường hợp 1 message
        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }

        // constuctor cho trường hợp nhiều message (thường là valid error)
        public ApiErrorResult(string[] validationErrors)
        {
            IsSuccessed = false;
            ValidationErrors = validationErrors;
        }
    }
}
