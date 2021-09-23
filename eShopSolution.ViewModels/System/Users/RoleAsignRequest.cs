using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.Users
{
    public class RoleAssignRequest
    {
        // Id của user
        public Guid Id { get; set; }    
        // list các quyền đc chọn cho user đó
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();   
    }
}
