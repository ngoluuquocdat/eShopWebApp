using eShopSolution.ApiIntegration;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductsController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IActionResult> Detail(int id, string culture)
        {
            var product = await _productApiClient.GetById(id, culture);
            return View(new ProductDetailViewModel()
            {
                Product = product   
            });
        }

        public async Task<IActionResult> Category(int category_id, string culture, int pageIndex=1, int pageSize=4)
        {
            // tham số culture của hàm được khai báo bên endpoint của StartUp
            // nên hàm sẽ nhận được luôn
            var products = await _productApiClient.GetPagings( new GetManageProductPagingRequest 
            {
                CategoryId = category_id,
                LanguageId = culture,
                PageIndex = pageIndex,
                PageSize = pageSize
            });
            return View(new ProductCategoryViewModel()
            {
                Category = await _categoryApiClient.GetById(culture, category_id),
                Products = products
            }) ;
        }
    }
}
