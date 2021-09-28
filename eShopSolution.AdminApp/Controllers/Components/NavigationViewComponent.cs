using eShopSolution.AdminApp.Models;
using eShopSolution.ApiIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers.Components
{
    // class này để load dữ liệu language id động lên _layout
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;
        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiClient = languageApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages_api_result = await _languageApiClient.GetAll();
            var navigationVm = new NavigationViewModel()
            {
                CurrentLanguageId = HttpContext.Session.GetString("DefaultLanguageId"),
                Languages = languages_api_result.ResultObj
            };
            return View("Default", navigationVm);
        }
    }
}
