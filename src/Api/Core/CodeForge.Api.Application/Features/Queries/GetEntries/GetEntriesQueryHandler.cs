using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Api.Application.Features.Queries.GetEntries;

public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public GetEntriesQueryHandler(IMapper mapper, IRepositoryManager manager)
    {
        _mapper = mapper;
        _manager = manager;
    }

    public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _manager.Entry.AsQueryable();

        if (request.TodaysEntries)
            query = query
                        .Where(e => e.CreatedDate >= DateTime.Now.Date)
                        .Where(e => e.CreatedDate <= DateTime.Now.AddDays(1).Date);

        query.Include(e => e.EntryComments)
            .OrderBy(e => Guid.NewGuid())
            .Take(request.Count);

        return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

    }
}
