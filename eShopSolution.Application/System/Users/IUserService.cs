﻿using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public interface IUserService
    {
        Task <ApiResult<string>> Authenticate(LoginRequest request);
        Task <ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);
        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUsersPagingRequest request);
        Task<ApiResult<UserVm>> GetById(Guid Id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}
