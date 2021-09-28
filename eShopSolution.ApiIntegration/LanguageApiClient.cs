using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class LanguageApiClient : BaseApiClient, ILanguageApiClient
    {
        public LanguageApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
        }


        public async Task<ApiResult<List<LanguageVm>>> GetAll()
        {
            return await GetAsync<ApiResult<List<LanguageVm>>>("/api/languages");
        }
    }
}
