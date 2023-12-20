using CodeForge.Api.Application.Interfaces.Repositories;
using CodeForge.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Api.Application.Features.Queries.SearchBySubject;


public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
{
    private readonly IRepositoryManager _manager;

    public SearchEntryQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
        // TODO validation, request.SearchText length should be checked

        var result = _manager.Entry.Get(i => EF.Functions.Like(i.Subject, $"{request.SearchText}%"), false, false);

        var searchResult = await result
            .Select(i => new SearchEntryViewModel
            {
                Id = i.Id,
                Subject = i.Subject
            })
            .ToListAsync(cancellationToken);

        return searchResult;
    }
}