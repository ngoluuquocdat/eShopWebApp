using eShopSolution.AdminApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Name này là nằm trong principal, tức là các claims,
            // => trong lúc tạo claims cho jwt trong backend api, phải có thuộc tính Name
            var user_name = User.Identity.Name;                                                    
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Language(NavigationViewModel viewModel)
        {
            // hàm này để đẩy DefaultLanguageId lên session mỗi khi click đổi ngôn ngữ
            // sau đó load lại trang Index
            HttpContext.Session.SetString("DefaultLanguageId", viewModel.CurrentLanguageId);

            return Redirect(viewModel.ReturnUrl);
        }
    }
}
