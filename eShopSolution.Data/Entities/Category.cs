using eShopSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }  // int? <==> nullable int
        public Status Status { get; set; }  // kiểu Status ở đây là 1 enum

        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
