using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.Infrastructure.Extensions;
using CodeForge.Common.ViewModels.Page;
using CodeForge.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Api.Application.Features.Queries.GetEntryComments;

internal class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PageViewModel<GetEntryCommentsViewModel>>
{

    private readonly IRepositoryManager _manager;

    public GetEntryCommentsQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<PageViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _manager.EntryComment.AsQueryable();

        query = query.Include(i => i.EntryCommentFavorites)
                     .Include(i => i.Owner)
                     .Include(i => i.EntryCommentVotes)
                     .Where(i => i.EntryId == request.EntryId);

        var list = query.Select(i => new GetEntryCommentsViewModel()
        {
            Id = i.Id,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryCommentFavorites.Any(j => j.OwnerId == request.UserId),
            FavoritedCount = i.EntryCommentFavorites.Count,
            CreatedDate = i.CreatedDate,
            CreatedByUserName = i.Owner.UserName,
            Vote =
                request.UserId.HasValue && i.EntryCommentVotes.Any(j => j.OwnerId == request.UserId)
                ? i.EntryCommentVotes.FirstOrDefault(j => j.OwnerId == request.UserId).VoteType
                : Common.ViewModels.VoteType.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }

}