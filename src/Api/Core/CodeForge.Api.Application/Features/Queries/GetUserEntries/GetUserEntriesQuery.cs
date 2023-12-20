using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForge.Common.ViewModels.Page;
using CodeForge.Common.ViewModels.Queries;
using MediatR;

namespace CodeForge.Api.Application.Features.Queries.GetUserEntries;

public class GetUserEntriesQuery : BasePagedQuery, IRequest<PageViewModel<GetUserEntriesDetailViewModel>>
{
    public Guid? UserId { get; set; }

    public string UserName { get; set; }

    public GetUserEntriesQuery(Guid? userId, string userName = null, int page = 1, int pageSize = 10) : base(page: page, pageSize: pageSize)
    {
        UserId = userId;
        UserName = userName;
    }
}
