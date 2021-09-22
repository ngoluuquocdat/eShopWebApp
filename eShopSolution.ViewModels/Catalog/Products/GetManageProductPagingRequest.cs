using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }          // keyword để tìm kiếm
        public List<int> CategoryIds { get; set; }   // mảng các categoryId
    }
}
