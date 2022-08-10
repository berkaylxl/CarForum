using CarForum.Common.Events.User;
using CarForum.Common.Exceptions;
using CarForum.Common.Infrastructure.Results;
using CarForum.Common.Models.Queries;
using CarForum.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarForum.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");
            return userDetail;
        }
        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");
            return userDetail;
        }
        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var res = await _httpClient.PostAsJsonAsync($"/api/user/update", user);
            return res.IsSuccessStatusCode;
        }
        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
            var httpResponse = await _httpClient.PostAsJsonAsync($"/api/User/ChangePassword", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlatternErrors;
                    throw new DatabaseValidationExcepton(responseStr);
                }
                return false;
            }
            return httpResponse.IsSuccessStatusCode;


        }
    }
}
