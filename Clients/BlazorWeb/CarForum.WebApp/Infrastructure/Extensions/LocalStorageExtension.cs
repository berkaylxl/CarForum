using Blazored.LocalStorage;

namespace CarForum.WebApp.Infrastructure.Extensions
{
    public static class LocalStorageExtension
    {
        public const string TokenName = "token";
        public const string UserName = "userName";
        public const string UserId= "userId";




        public static bool IsUserLoggedIn(this ISyncLocalStorageService localStorageService)
        {
            return !string.IsNullOrEmpty(GetToken(localStorageService));
        }
        public static string GetUserName(this ISyncLocalStorageService localStorageService)
        {
            return localStorageService.GetItem<string>(UserName);
        }
        //public static async Task<string> GetUserName(this ISyncLocalStorageService localStorageService)
        //{
        //    return await localStorageService.GetItemAsync<string>(UserName);
        //}

        public static void SetUserName(this ISyncLocalStorageService localStorageService ,string value)
        {
            localStorageService.SetItem(UserName, value);
        }
        //public static async Task SetUserName(this ISyncLocalStorageService localStorageService, string value)
        //{
        //     await localStorageService.SetItemAsync(UserName, value);
        //}
      
        public static Guid GetUserId (this ISyncLocalStorageService localStorageService)
        {
           return localStorageService.GetItem<Guid>(UserId);
        }
        public static void SetUserId(this ISyncLocalStorageService localStorageService,Guid id)
        {
            localStorageService.SetItem(UserId,id);
        }
        public static string GetToken(this ISyncLocalStorageService localStorageService )
        {
            var token =  localStorageService.GetItem<string>(TokenName);
            return token;
        }
        public static void  SetToken(this ISyncLocalStorageService localStorageService,string value)
        {
           localStorageService.SetItem(TokenName,value);
        }
        //public static async Task SetToken(this ISyncLocalStorageService localStorageService, string value)
        //{
        //    localStorageService.SetItemAsync(TokenName, value);
        //}
    }
}
