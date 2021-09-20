using eShopSolution.ViewModels.System.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Service
{
    public class UserApiClient : IUserApiClient
    {
        // IHttpClientFactory để nó tạo ra 1 đối tượng client
        private readonly IHttpClientFactory _httpClientFactory;
        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var request_json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(request_json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            // kiểu (post, put,...) và link thì tương ứng với method của API
            client.BaseAddress = new Uri("https://localhost:5001");
            var response = await client.PostAsync("/api/users/authenticate", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }
    }
}
