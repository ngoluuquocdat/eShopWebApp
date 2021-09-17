using eShopSolution.Application.Catalog.Products;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]     // view sẽ gọi đến đây, thay tên controller vào
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;       

        //constructor
        public ProductController(IPublicProductService publicProductService)
        {
            _publicProductService = publicProductService;
        }


        [HttpGet]   // method ở dưới sẽ là kiểu HttpGet
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }

        // nghịch
        //private readonly EShopDbContext _context;
        // constructor nghịch
        //public ProductController(EShopDbContext context)
        //{
        //    _context = context;
        //}
        // nghịch
        //[HttpGet]
        //public async Task<IActionResult> GetAllatController()
        //{
        //    var query = from p in _context.Products
        //                join pt in _context.ProductTranslations on p.Id equals pt.ProductId
        //                join pic in _context.ProductInCategories on p.Id equals pic.ProductId
        //                join c in _context.Categories on pic.CategoryId equals c.Id
        //                select new { p, pt, pic };

        //    var data = await query.Select(x => new ProductViewModel()
        //    {
        //        Id = x.p.Id,
        //        Name = x.pt.Name,
        //        DateCreated = x.p.DateCreated,
        //        Description = x.pt.Description,
        //        Details = x.pt.Details,
        //        LanguageId = x.pt.LanguageId,
        //        OriginalPrice = x.p.OriginalPrice,
        //        Price = x.p.Price,
        //        SeoAlias = x.pt.SeoAlias,
        //        SeoDescription = x.pt.SeoDescription,
        //        SeoTitle = x.pt.SeoTitle,
        //        Stock = x.p.Stock,
        //        ViewCount = x.p.ViewCount
        //    }).ToListAsync();

        //    return Ok(data);
        //}
    }
}
