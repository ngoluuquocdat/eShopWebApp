using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        // các hàm dùng chung cho các request Get

        // hàm get về 1 object
        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {

            // lấy token từ session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");

            // add token vào header Authentication của request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync(url);
            var result_json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(result_json,
                    typeof(TResponse));

                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<TResponse>(result_json);
        }

        // hàm get về 1 list obj
        // không dùng chung với 1 obj đc vì list sẽ phải Deserialize kiểu khác
        public async Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false)
        {
            // lấy token từ session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync(url);
            var result_json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = (List<T>)JsonConvert.DeserializeObject(result_json, typeof(List<T>));
                return data;
            }
            throw new Exception(result_json);
        }
    }
}
