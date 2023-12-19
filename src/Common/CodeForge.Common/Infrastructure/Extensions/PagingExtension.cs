using CodeForge.Common.ViewModels.Page;
using Microsoft.EntityFrameworkCore;

namespace CodeForge.Common.Infrastructure.Extensions;

public static class PagingExtension
{
    public static async Task<PageViewModel<T>> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
    {
        var count = await query.CountAsync();

        Page paging = new(currentPage, pageSize, count);
        var data = await query.Skip(paging.Skipped).Take(paging.PageSize).AsNoTracking().ToListAsync();

        PageViewModel<T> result = new(data, paging);

        return result;
    }
}
