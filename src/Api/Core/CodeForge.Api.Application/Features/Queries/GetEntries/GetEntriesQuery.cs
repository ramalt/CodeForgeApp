using CodeForge.Common.ViewModels.Queries;
using MediatR;

namespace CodeForge.Api.Application.Features.Queries.GetEntries;

public class GetEntriesQuery : IRequest<List<GetEntriesViewModel>>
{
    public bool TodaysEntries { get; set; }
    public int Count { get; set; } = 10;
    
}
