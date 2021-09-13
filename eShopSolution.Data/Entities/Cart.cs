﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Entities
{
    public class Cart
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }  // price này có thể khác product.Price, nếu có khuyến mãi
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }

        // navigation property
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
