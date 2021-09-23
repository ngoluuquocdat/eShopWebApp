using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Service
{
    public class RoleApiClient : IRoleApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<List<RoleVm>>> GetAll()
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            
            var response = await client.GetAsync($"/api/roles");

            var result_json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // phải chuyển kiểu này chứ như return dưới cùng là nó 
                // không chuyển kiểu json -> list được
                List<RoleVm> myDeserializedObjList = JsonConvert.DeserializeObject<List<RoleVm>>(result_json);
                return new ApiSuccessResult<List<RoleVm>>(myDeserializedObjList);
                //Don't use: return JsonConvert.DeserializeObject<ApiSuccessResult<List<RoleVm>>>(result_json);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleVm>>>(result_json);
        }
    }
}
