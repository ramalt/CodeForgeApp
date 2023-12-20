using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private readonly IRepositoryManager _manager;

    public GetEntryDetailQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        var query = _manager.Entry.AsQueryable();

        query = query.Include(i => i.EntryFavorites)
                     .Include(i => i.Owner)
                     .Include(i => i.EntryVotes)
                     .Where(i => i.Id == request.EntryId);

        var list = query.Select(i => new GetEntryDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.OwnerId == request.UserId),
            FavoritedCount = i.EntryFavorites.Count,
            CreatedDate = i.CreatedDate,
            OwnerUserName = i.Owner.UserName,
            Vote =
                    request.UserId.HasValue && i.EntryVotes.Any(j => j.OwnerId == request.UserId)
                    ? i.EntryVotes.FirstOrDefault(j => j.OwnerId == request.UserId).VoteType
                    : Common.ViewModels.VoteType.None
        });

        return await list.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}