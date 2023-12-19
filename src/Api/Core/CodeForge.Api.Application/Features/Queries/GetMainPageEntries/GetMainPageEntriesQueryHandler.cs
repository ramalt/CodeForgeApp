using AutoMapper;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.Infrastructure.Extensions;
using CodeForge.Common.ViewModels.Page;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Common.ViewModels.Queries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PageViewModel<GetEntryDetailViewModel>>
{
    private readonly IRepositoryManager _manager;

    public GetMainPageEntriesQueryHandler(IMapper mapper, IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<PageViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _manager.Entry.AsQueryable();

        query = query.Include(e => e.EntryFavorites)
                     .Include(e => e.Owner)
                     .Include(e => e.EntryVotes);

        var list = query.Select(e => new GetEntryDetailViewModel()
        {
            Id = e.Id,
            Subject = e.Subject,
            Content = e.Content,
            IsFavorited = request.UserId.HasValue && e.EntryFavorites.Any(ef => ef.OwnerId == request.UserId),
            FavoritedCount = e.EntryFavorites.Count,
            CreatedDate = e.CreatedDate,
            OwnerUserName = e.Owner.UserName,
            Vote = request.UserId.HasValue && e.EntryVotes.Any(ev => ev.OwnerId == request.UserId)
                                                                                ? e.EntryVotes.FirstOrDefault(v => v.OwnerId == request.UserId).VoteType
                                                                                : VoteType.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;



    }
}
