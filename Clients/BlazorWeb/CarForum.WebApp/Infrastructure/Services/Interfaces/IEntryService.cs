using CarForum.Common.Models.Page;
using CarForum.Common.Models.Queries;
using CarForum.Common.Models.RequestModels;

namespace CarForum.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Guid>CreateEntry(CreateEntryCommand command);
        Task<Guid> CreateEntryComment(CreateEntryCommand command);
        Task<List<GetEntriesViewModel>> GetEntries();
        Task<PagedViewModel<GetEntryDetailViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);
        Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null);
        Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);
    }
}