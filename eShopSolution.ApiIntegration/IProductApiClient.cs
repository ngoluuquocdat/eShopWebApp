using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetPagings(GetManageProductPagingRequest request);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<ProductViewModel> GetById(int id, string languageId);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);

        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);

    }
}
