using CodeForge.Common.ViewModels.Page;
using MediatR;

namespace CodeForge.Common.ViewModels.Queries;

public class GetMainPageEntriesQuery : BasePagedQuery, IRequest<PageViewModel<GetEntryDetailViewModel>>
{
    public Guid? UserId { get; set; }

    public GetMainPageEntriesQuery(int pageSize, int page, Guid? userId) : base(pageSize, page)
    {
        UserId = userId;
    }
}
