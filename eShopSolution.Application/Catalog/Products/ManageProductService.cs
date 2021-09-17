
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.Common;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;   // chỉ đọc
        private readonly IStorageService _storageService;
        public ManageProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;     // _context sẽ chỉ đc gán 1 lần duy nhất ở đây
            _storageService = storageService;
        }

        public async Task<int> AddImages(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)      // nếu có file ảnh thì mới add
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
                _context.ProductImages.Add(productImage);
            }
            
            return await _context.SaveChangesAsync();
            
        }

        public async Task AddViewCount(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                // thêm mới dạng cha con như ở dưới: vì Id của new product lúc này chưa có trong Db
                // nên có thể dùng cách này, khóa ngoại ProducId ở ProductTranslation sẽ
                // được tự động map với Id của new product
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation
                    {
                        // không cần khởi tạo khóa ngoại ProductId
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }

            };
            // set thumbnail image for product
            if(request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();   // trả về số bảng ghi đc thêm vào Db (?)
        }

        public async Task<int> Delete(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null) throw new EShopException($"Cannot find a product with Id: {ProductId}");
            // trước khi xóa product thì phải xóa ảnh của nó trong storage
            var images =  _context.ProductImages
                    .Where(i => i.ProductId == ProductId);
            foreach(var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            } 
            // sau khi xóa hết ảnh ở storage, thì xóa product trong Db
            _context.Products.Remove(product);
            
            return await _context.SaveChangesAsync();       // trả về số bảng ghi đc xóa khỏi Db (?)
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
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
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));    
                // x ở đây là 1 bảng ghi trong query.
                // 1 bảng ghi trong query bây giờ có 3 thứ:
                // product, productTranslation và productInCategory, theo select ở dòng 82 (hover vào x để kiếm chứng)

            if (request.CategoryIds.Count > 0)   // nếu có bất kì tìm kiếm nào về Category
                query = query.Where(x => request.CategoryIds.Contains(x.pic.CategoryId));
            // dòng 86, 87 có nghĩa là: lấy các product có category nằm trong đống request.category
            // x chỗ này vẫn là 1 bảng ghi trong query

            // ------------bước 3. paging - phân trang ---------------------
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1)*request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()     // x chỗ này vẫn là 1 bảng ghi trong query
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
                }).ToListAsync();   // toListAsync ở đây nên phải có await ở đầu nhé (dòng 98)
            /* giải thích dòng 93:
             * Skip(n): bỏ qua n bảng ghi rồi mới bắt đầu lấy
             *          hàm này trả về các bảng ghi ngay sau index input - là n
             * Take(n): lấy n bảng ghi từ đầu của nguồn dữ liệu
             * ==> ví dụ mỗi page chứa 10 records, 
             * cần lấy data vào page 1: 
             * Skip(0*10).Take(10): kết quả sẽ trả ra 10 bảng ghi đầu tiên trong Db
             * cần lấy data vào page 2:
             * Skip(1*10).Take(10): kết quả sẻ trả ra 10 bảng ghi, tính từ bảng 11
             *                      như thế này là page 2 đã có 10 bảng ghi mới, ko dính gì 
             *                      đến 10 bảng ghi đầu tiên của page 1 nữa
            */

            // ------------bước 4. select and projection ---------------------
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null) throw new EShopException($"Cannot find a product with Id: {ProductId}");
            var images = _context.ProductImages.Where(i => i.ProductId == ProductId)
                .Select(i => new ProductImageViewModel()
                {
                    Id = i.Id,
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
            return await images;
        }

        public async Task<int> RemoveImages(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if(productImage == null)
                throw new EShopException($"Cannot find an image with id {imageId}");
            // xóa file vật lý
            await _storageService.DeleteFileAsync(productImage.ImagePath);
            // xóa record trong db
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            // thật ra ở đây là update ProductTranslation
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations
                .FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if(product == null || productTranslation == null) 
                throw new EShopException($"Cannot find a product with Id: {request.Id}");

            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;

            // sửa thumbnail image for product
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages
                    .FirstOrDefaultAsync(i => i.ProductId==request.Id && i.IsDefault == true);

                if(thumbnailImage != null)      // nếu đã có thumbnail rồi thì sửa thumbnail
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }    
            }
            return await _context.SaveChangesAsync();   
        }

        public async Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new EShopException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int ProductId, decimal new_price)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
                throw new EShopException($"Cannot find a product with Id: {ProductId}");
            product.Price = new_price;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int ProductId, int addQuantity)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
                throw new EShopException($"Cannot find a product with Id: {ProductId}");
            product.Stock += addQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
