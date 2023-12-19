namespace CodeForge.Common.ViewModels.Page;

public class PageViewModel<T> where T : class
{
    public IList<T> Result { get; set; }
    public Page PageInfo { get; set; }

    public PageViewModel(IList<T> results, Page pageInfo)
    {
        PageInfo = pageInfo;
        Result = results;
    }
    public PageViewModel() : this(new List<T>(), new Page())
    {

    }
}
