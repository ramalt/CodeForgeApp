using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForge.WebApp.Infrastructure.Services.Interfaces;

namespace CodeForge.WebApp.Infrastructure.Services;

public class FavService : IFavService
{
    private readonly HttpClient client;

    public FavService(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateEntryFav(Guid entryId)
    {
        await client.PostAsync($"/api/favorite/Entry/{entryId}", null);
    }

    public async Task CreateEntryCommentFav(Guid entryCommentId)
    {
        await client.PostAsync($"/api/favorite/EntryComment/{entryCommentId}", null);
    }

    public async Task DeleteEntryFav(Guid entryId)
    {
        await client.PostAsync($"/api/favorite/entry/{entryId}", null);
    }

    public async Task DeleteEntryCommentFav(Guid entryCommentId)
    {
        await client.PostAsync($"/api/favorite/entry/comment/{entryCommentId}", null);
    }
}
