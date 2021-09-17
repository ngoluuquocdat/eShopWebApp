using eShopSolution.ViewModels.Catalog.Common;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        // thêm mới product
        Task<int> Create(ProductCreateRequest request);    // trả về số bảng ghi đc thêm, 
                                                           // Task để  dùng context.SaveChangesAsync()

        // chỉnh sửa product: ở đây chỉ chỉnh sửa các thông tin mô tả
        Task<int> Update(ProductUpdateRequest request);

        // xóa product
        Task<int> Delete(int ProductId);

        // cập nhật giá
        Task<bool> UpdatePrice(int ProductId, decimal new_price);

        // cập nhật số lượng tồn kho
        Task<bool> UpdateStock(int ProductId, int addQuantity);

        // tăng lượng view, ++1
        Task AddViewCount(int ProductId);   // này trả về void
 
        
        // method hiển thị product theo page (?)
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        // method thêm ảnh vào list ảnh của 1 product
        Task<int> AddImages(int productId, ProductImageCreateRequest request);

        // method xoá ảnh khỏi list ảnh của 1 product
        Task<int> RemoveImages(int imageId);

        // method update nội dung của ảnh   (ko làm update file ảnh nữa vì có delete với add rồi)
        Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request);

        // method get list ảnh của 1 product
        Task<List<ProductImageViewModel>> GetListImages(int productId);
    }
}
