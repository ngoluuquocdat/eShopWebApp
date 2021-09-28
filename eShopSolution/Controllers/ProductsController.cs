using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Detail(int product_id)
        {
            return View();
        }

        public IActionResult Category(int category_id)
        {
            return View();
        }
    }
}
