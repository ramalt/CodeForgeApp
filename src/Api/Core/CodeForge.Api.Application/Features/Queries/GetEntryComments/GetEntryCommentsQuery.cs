using CodeForge.Common.ViewModels.Page;
using CodeForge.Common.ViewModels.Queries;
using MediatR;

namespace CodeForge.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQuery : BasePagedQuery, IRequest<PageViewModel<GetEntryCommentsViewModel>>
{
    public GetEntryCommentsQuery(Guid entryId, Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }


    public Guid EntryId { get; set; }

    public Guid? UserId { get; set; }
}
