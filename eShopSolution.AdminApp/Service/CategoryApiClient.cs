﻿using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Service
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, httpContextAccessor)
        {
        }

        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            return await GetListAsync<CategoryVm>("/api/categories?languageId=" + languageId);
        }
    }
}
