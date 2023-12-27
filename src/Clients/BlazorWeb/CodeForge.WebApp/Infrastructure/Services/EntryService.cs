using System.Net.Http.Json;
using System.Security.Authentication;
using CodeForge.Common.ViewModels.Page;
using CodeForge.Common.ViewModels.Queries;
using CodeForge.Common.ViewModels.RequestModels;
using CodeForge.WebApp.Infrastructure.Services.Auth;
using CodeForge.WebApp.Infrastructure.Services.Interfaces;

namespace CodeForge.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly HttpClient _client;
    private readonly IIdentityService _identity;

    public EntryService(HttpClient client, IIdentityService identity)
    {
        _client = client;
        _identity = identity;
    }
    public async Task<Guid> CreateEntry(CreateEntryCommand command)
    {
        command.OwnerId = _identity.GetUserId();
        
        
        var res = await _client.PostAsJsonAsync("/api/Entry", command);

        if (!res.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await res.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
    {
        command.OwnerId = _identity.GetUserId();

        var res = await _client.PostAsJsonAsync("/api/Entry/comment", command);

        if (!res.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await res.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<List<GetEntriesViewModel>> GetEntires()
    {
        var result = await _client.GetFromJsonAsync<List<GetEntriesViewModel>>("api/Entry?TodaysEntries=false&Count=30");
        return result;
    }

    public async Task<PageViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var result = await _client.GetFromJsonAsync<PageViewModel<GetEntryCommentsViewModel>>($"/api/entry/comment/{entryId}?page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
    {
        var result = await _client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");

        return result;
    }

    public async Task<PageViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var result = await _client.GetFromJsonAsync<PageViewModel<GetEntryDetailViewModel>>($"/api/entry/subject?page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<PageViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
    {
        userName = _identity.GetUserName();
        var result = await _client.GetFromJsonAsync<PageViewModel<GetEntryDetailViewModel>>($"/api/entry/user?userName={userName}&page={page}&pageSize={pageSize}");

        return result;
    }

    public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
    {
        var result = await _client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");

        return result;
    }
}
