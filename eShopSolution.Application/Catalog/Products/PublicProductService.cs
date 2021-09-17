
using eShopSolution.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModels.Catalog.Common;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.Application.Common;

namespace eShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _context;   // chỉ đọc
        public PublicProductService(EShopDbContext context)
        {
            _context = context;     // _context sẽ chỉ đc gán 1 lần duy nhất ở đây            
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };

            var data = await query.Select(x => new ProductViewModel()  
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            // ------------bước 1. Select join ---------------------
            // vì là tìm theo tên (keyword), mà tên thì nằm trong ProductTranslation,
            // nên ở đây phải dùng Inner Join:
            // bảng products (p) join với bảng productTranslations (pt)
            // bảng products join với bảng productInCategories (pic)
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            // ------------bước 2. Filter ---------------------
            // x ở đây là 1 bảng ghi trong query.
            // 1 bảng ghi trong query bây giờ có 3 thứ:
            // product, productTranslation và productInCategory, theo select ở dòng 82 (hover vào x để kiếm chứng)

            if (request.CategoryId.HasValue && request.CategoryId.Value>0)   // nếu có bất kì tìm kiếm nào về Category
                query = query.Where(x => x.pic.CategoryId == request.CategoryId);


            // ------------bước 3. paging - phân trang ---------------------
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new ProductViewModel()     // x chỗ này vẫn là 1 bảng ghi trong query
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();


            // ------------bước 4. select and projection ---------------------
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
