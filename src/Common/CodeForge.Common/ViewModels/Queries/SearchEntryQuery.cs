using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CodeForge.Common.ViewModels.Queries;

public class SearchEntryQuery : IRequest<List<SearchEntryViewModel>>
{
    public string SearchText { get; set; }

    public SearchEntryQuery()
    {

    }

    public SearchEntryQuery(string searchText)
    {
        SearchText = searchText;
    }
}