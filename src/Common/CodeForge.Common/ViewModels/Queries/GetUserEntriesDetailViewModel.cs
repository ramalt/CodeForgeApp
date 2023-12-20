using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeForge.Common.ViewModels.Queries;

public class GetUserEntriesDetailViewModel : BaseFooterFavoritedViewModel
{
    public Guid Id { get; set; }

    public string Subject { get; set; }

    public string Content { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedByUserName { get; set; }
}