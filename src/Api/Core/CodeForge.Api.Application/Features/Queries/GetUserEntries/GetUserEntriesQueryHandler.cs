using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.Infrastructure.Extensions;
using CodeForge.Common.ViewModels.Page;
using CodeForge.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Api.Application.Features.Queries.GetUserEntries;

public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PageViewModel<GetUserEntriesDetailViewModel>>
{
    private readonly IRepositoryManager _manager;

    public GetUserEntriesQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<PageViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _manager.Entry.AsQueryable();

        if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
        {
            query = query.Where(i => i.OwnerId == request.UserId);
        }
        else if (!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(i => i.Owner.UserName == request.UserName);
        }
        else
            return null;

        query = query.Include(i => i.EntryFavorites)
                     .Include(i => i.Owner);

        var list = query.Select(i => new GetUserEntriesDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = false,
            FavoritedCount = i.EntryFavorites.Count,
            CreatedDate = i.CreatedDate,
            CreatedByUserName = i.Owner.UserName
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}