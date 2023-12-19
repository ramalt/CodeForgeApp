namespace CodeForge.Common.ViewModels.Page;

public class BasePagedQuery
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public BasePagedQuery(int pageSize, int page)
    {
        PageSize = pageSize;
        Page = page;
    }

}
