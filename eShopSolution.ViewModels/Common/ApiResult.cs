using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }

        public string Message { get; set; } // nếu không thành công thì có message trả về 

        public T ResultObj { get; set; }    // nếu thành công thì trả về 1 obj 

        public ApiResult() { }

        // constructor cho trường hợp [Failed]
        public ApiResult(bool isSuccessed, string message)
        {
            IsSuccessed = isSuccessed;
            Message = message;
        }

        // constructor cho trường hợp [Success]
        public ApiResult(bool isSuccessed, string message, T resultObj)
        {
            IsSuccessed = isSuccessed;
            Message = message;
            ResultObj = resultObj;
        }
    }
}
