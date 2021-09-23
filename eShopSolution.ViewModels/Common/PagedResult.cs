using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class PagedResult<T> : PagedResultBase  // kiểu T: generic. dùng kiểu generic để class này dùng đc với
                                 // nhiều module khác nhau
    {
        public List<T> Items { set; get; }      // list các item trên trang    
    }
}
