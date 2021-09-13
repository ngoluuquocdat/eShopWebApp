using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class ProductInCategory
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        // 2 thuộc tính liên kết tới Product và Category
        public Product Product { get; set; }

        public Category Category { get; set; }
    }
}
