using CarForum.Common.Models.Page;
using CarForum.Common.Models.Queries;
using CarForum.Common.Models.RequestModels;
using CarForum.WebApp.Infrastructure.Interfaces;
using System.Net.Http.Json;

namespace CarForum.WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient _client;

        public EntryService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries()
        {
            var result = await _client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysEnties=false&count=30");
            return result;
        }
        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var result = await _client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");
            return result;
        }
        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pageSize={pageSize}");
            return result;
        }
        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");
            return result;
        }
        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/comments/{entryId}?page={page}&pageSize={pageSize}");
            return result;
        }
        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var result = await _client.PostAsJsonAsync("/api/Entry/CreateEntry", command);
            if (!result.IsSuccessStatusCode)
                return Guid.Empty;
            var guidStr = await result.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));

        }
        public async Task<Guid> CreateEntryComment(CreateEntryCommand command)
        {
            var result = await _client.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);
            if (!result.IsSuccessStatusCode)
                return Guid.Empty;
            var guidStr = await result.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }
        public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        {
            var result = await _client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");
            return result;
        }
    }
}
