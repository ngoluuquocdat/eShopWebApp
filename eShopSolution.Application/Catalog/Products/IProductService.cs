using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IProductService
    {
        // thêm mới product
        Task<int> Create(ProductCreateRequest request);    // trả về số bảng ghi đc thêm, 
                                                           // Task để  dùng context.SaveChangesAsync()

        // chỉnh sửa product: ở đây chỉ chỉnh sửa các thông tin mô tả
        Task<int> Update(ProductUpdateRequest request);

        // xóa product
        Task<int> Delete(int ProductId);

        // get product by Id
        Task<ProductViewModel> GetById(int ProductId, string languageId);

        // cập nhật giá
        Task<bool> UpdatePrice(int ProductId, decimal new_price);

        // cập nhật số lượng tồn kho
        Task<bool> UpdateStock(int ProductId, int addQuantity);

        // tăng lượng view, ++1
        Task AddViewCount(int ProductId);   // này trả về void
 
        
        // method hiển thị product theo page (?)
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        // method thêm ảnh vào list ảnh của 1 product
        Task<int> AddImage(int productId, ProductImageCreateRequest request);

        // method xoá ảnh khỏi list ảnh của 1 product
        Task<int> RemoveImage(int imageId);

        // method update ảnh theo id ảnh  
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        // method get list ảnh của 1 product
        Task<List<ProductImageViewModel>> GetListImages(int productId);

        // method get 1 ảnh theo id
        Task<ProductImageViewModel> GetImageById(int imageId);

        // method get products by category dành cho Public
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);
        // GetProductPagingRequest lúc này là class ở public, không phải ở manage

        // method gán category cho sản phẩm
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        // method get các sản phẩm featured
        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);
    }
}
