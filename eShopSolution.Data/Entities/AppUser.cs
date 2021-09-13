using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class AppUser : IdentityUser<Guid>   // Guid: là kiểu của khóa chính, guid là duy nhất cho toàn hệ thống(?)
    {
        // các fields khác như Id, Phone, Email,... đã có sẵn trong class IdentityUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        // navigation properties
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
