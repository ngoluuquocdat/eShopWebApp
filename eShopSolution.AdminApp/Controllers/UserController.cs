using eShopSolution.ApiIntegration;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IRoleApiClient _roleApiClient;
        private readonly IConfiguration _configuration;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration,
                                IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex=1, int pageSize=2)
        {

            var request = new GetUsersPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _userApiClient.GetUsersPaging(request);
            ViewBag.Keyword = keyword;  // này để vẫn lưu keyword trên text field sau khi bấm 'tìm kiếm'
            if (TempData["result"] != null)
                ViewBag.SuccessMessage = TempData["result"];
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Tạo mới thành công";
                return RedirectToAction("Index");
            }                   

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // hàm này dùng để lấy thông tin user ra trước khi điền thông tin update
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                // tạo 1 update request với các giá trị ban đầu là copy từ thằng user tìm ra
                // phải tạo update request vì để bên View có set model là UserUpdateRequest
                // và từ đó đẩy UserUpdateRequest xuống hàm Post Update
                var updateRequest = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");   // lỗi thì ra trang Error có sẵn trong HomeController
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            // hàm này để thực hiện update user theo request
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update người dùng thành công";
                return RedirectToAction("Index");
            }
                

            ModelState.AddModelError("", result.Message);
            return View(request);
        }


        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            // cũng cần 2 hàm tương tự Update
            return View(new UserDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid Id)
        {
            // cũng 2 hàm như update/delete
            // hàm này ra 1 RoleAssignRequest với các giá trị ban đầu là
            // thông tin role assign sẵn có của 1 user 
            var roleAssignRequest = await GetRoleAssignRequest(Id);
            return View(roleAssignRequest);
            //return View(new RoleAssignRequest());
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RoleAssign(request.Id, request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }


        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid Id)
        {
            // hàm để tạo 1 RoleAssignRequest
            var userObj = await _userApiClient.GetById(Id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            roleAssignRequest.Id = Id;      // gán user id vào Id của RoleAssignRequest
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    // trả về true nếu list Roles của User có role.Name này
                    Selected = userObj.ResultObj.Roles.Contains(role.Name)

                });
            }
            return roleAssignRequest;
        }      
    }
}
