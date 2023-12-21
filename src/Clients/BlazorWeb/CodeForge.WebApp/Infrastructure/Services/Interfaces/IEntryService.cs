using CodeForge.Common.ViewModels.Page;
using CodeForge.Common.ViewModels.Queries;
using CodeForge.Common.ViewModels.RequestModels;

namespace CodeForge.WebApp.Infrastructure.Services.Interfaces;

public interface IEntryService
{
    Task<List<GetEntriesViewModel>> GetEntires();

    Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);

    Task<PageViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);

    Task<PageViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null);

    Task<PageViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);


    Task<Guid> CreateEntry(CreateEntryCommand command);

    Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);

    Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);

}
