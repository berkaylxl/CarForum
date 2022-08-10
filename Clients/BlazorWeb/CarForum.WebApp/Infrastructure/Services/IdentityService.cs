using Blazored.LocalStorage;
using CarForum.Common.Exceptions;
using CarForum.Common.Infrastructure.Results;
using CarForum.Common.Models.Queries;
using CarForum.Common.Models.RequestModels;
using CarForum.WebApp.Infrastructure.Extensions;
using CarForum.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarForum.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _client;
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService)
        {
            _client = client;
            this.syncLocalStorageService = syncLocalStorageService;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }
        public string GetUserName()
        {
            return syncLocalStorageService.GetToken();
        }
        public Guid GetUserId()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await _client.PostAsJsonAsync("/api/User/Login", command);
            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlatternErrors;
                    throw new DatabaseValidationExcepton(responseStr);

                }
                return false;
            }
            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token))
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUserName(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.UserName);
                return true;
            }
            return false;
        }
        public void LogOut()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
            syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);
            _client.DefaultRequestHeaders.Authorization = null;
        }









    }
}
