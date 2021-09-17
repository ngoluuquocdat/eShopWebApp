using eShopSolution.Application.Catalog.Products;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.ProductImages;
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
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;

        //constructor
        public ProductsController(IPublicProductService publicProductService,
            IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }
        // ------------------------------METHODs FOR PUBLIC-----------------------------

        // http://localhost:port/products?pageIndex=1&pageSize=10&CategoryId=1
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        // [FromQuery]: chỉ định rằng các tham số của hàm được lấy từ url/query(?)
        {
            var products = await _publicProductService.GetAllByCategoryId(languageId,request);
            return Ok(products);
        }

        // -----------------------------------------------------------------------------

        // ------------------------------METHODs FOR MANAGE-----------------------------

        // http://localhost:port/products/1/vi-VN
        [HttpGet("{productId}/{languageId}")]    
        // truyền thêm productId trong link, 
        // thành phần này phải trùng luôn với tên của param trong hàm thì hàm mới xác định được
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        // [FromBody]: chỉ định rằng tham số được lấy từ body của request gởi đến 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int productId = await _manageProductService.Create(request);
            if (productId == 0)
                return BadRequest();    // trạng thái lỗi 400
            var new_product = await _manageProductService.GetById(productId, request.LanguageId);
            //return Created(nameof(GetById), new_product);       // trạng thái 201, trả về 1 url và 1 obj
            return CreatedAtAction(nameof(GetById), new { id = productId}, new_product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int affectedRecords = await _manageProductService.Update(request);
            if (affectedRecords == 0)
                return BadRequest();    // trạng thái lỗi 400            
            return Ok("Updated successfully!");
        }

        [HttpDelete("{productId}")]   
        // ở dưới ko có fromBody hay gì cả, nên trong link phải có productId, 
        // trùng luôn với tên của param trong hàm thì hàm mới xác định được
        public async Task<IActionResult> Delete(int productId)
        {
            int affectedRecords = await _manageProductService.Delete(productId);
            if (affectedRecords == 0)
                return BadRequest();    // trạng thái lỗi 400            
            return Ok("Deleted successfully!");
        }

        [HttpPatch("{productId}/{newPrice}")]     // httpPatch: update 1 phần bản ghi
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            bool isSuccess = await _manageProductService.UpdatePrice(productId, newPrice);
            if (!isSuccess)
                return BadRequest();    // trạng thái lỗi 400            
            return Ok("Updated Price successfully!");
        }

        // Api for Image
        

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        // [FromBody]: chỉ định rằng tham số được lấy từ body của request gởi đến 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int imageId = await _manageProductService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();    // trạng thái lỗi 400
            var new_image = await _manageProductService.GetImageById(imageId);           
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, new_image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int affectedRecords = await _manageProductService.UpdateImage(imageId, request);
            if (affectedRecords == 0)
                return BadRequest();    // trạng thái lỗi 400
            return Ok("Image updated successfully!");
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int affectedRecords = await _manageProductService.RemoveImage(imageId);
            if (affectedRecords == 0)
                return BadRequest();    // trạng thái lỗi 400
            return Ok("Image deleted!");
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
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
