﻿using CarForum.Common.Models.Queries;

namespace CarForum.WebApp.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
        Task<UserDetailViewModel> GetUserDetail(Guid? id);
        Task<UserDetailViewModel> GetUserDetail(string userName);
        Task<bool> UpdateUser(UserDetailViewModel user);
    }
}