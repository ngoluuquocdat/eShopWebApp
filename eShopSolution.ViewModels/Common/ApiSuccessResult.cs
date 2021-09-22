using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        // constructor cho trường hợp success + trả về obj gì đó 
        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        // constuctor cho trường hợp success 
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
