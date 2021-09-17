using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Common
{
    public class PagingRequestBase
    {
        public int PageIndex { get; set; }  // index của trang, VD: page 1, page 2,...
        public int PageSize { get; set; }   // kích cỡ của trang
    }
}
