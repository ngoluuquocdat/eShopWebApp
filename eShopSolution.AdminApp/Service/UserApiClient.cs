using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Service
{
    public class UserApiClient : IUserApiClient
    {
        // IHttpClientFactory để nó tạo ra 1 đối tượng client
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        // các methods gọi về Api
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var request_json = JsonConvert.SerializeObject(request);

            // body của request
            var httpContent = new StringContent(request_json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            // kiểu (post, put,...) và link thì tương ứng với method của API
            client.BaseAddress = new Uri("https://localhost:5001");
            var response = await client.PostAsync("/api/users/authenticate", httpContent);

            var result_json = await response.Content.ReadAsStringAsync();

            // kiểm tra trạng thái thành công để convert từ json về đúng kiểu
            if (response.IsSuccessStatusCode)
            {               
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(result_json);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(result_json);
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/users/{id}");
            var result_json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result_json);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result_json);
        }

        public async Task<ApiResult<UserVm>> GetById(Guid Id)
        {
            // lấy token từ session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            
            // add token vào header Authentication của request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/users/{Id}");
            var result_json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserVm>>(result_json);

            return JsonConvert.DeserializeObject<ApiErrorResult<UserVm>>(result_json);
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUsersPagingRequest request)
        {
            // ko phải post nên ko cần đẩy request_json lên nữa


            // lấy token từ session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            // add token vào header Authentication của request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            // url phải có param trùng với param định nghĩa ở API
            var response = await client.GetAsync($"/api/users/paging?" + 
                $"pageIndex={request.PageIndex}&pageSize={request.PageSize}"+
                $"&keyword={request.Keyword}");

            var result_json = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserVm>>>(result_json);

            return users;
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            var json = JsonConvert.SerializeObject(request);

            // body của request
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/users/register", httpContent);

            var result_json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result_json);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result_json);
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid Id, RoleAssignRequest request)
        {
            // lấy token từ session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            // add token vào header Authentication của request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var request_json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(request_json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/users/{Id}/roles", httpContent);

            var result_json = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result_json);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result_json);
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/users/{id}", httpContent);

            var result_json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result_json);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result_json);
        }
    }
}
