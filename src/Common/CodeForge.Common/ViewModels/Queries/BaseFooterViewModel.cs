namespace CodeForge.Common.ViewModels.Queries;

public class BaseFooterRateViewModel
{
    public VoteType Vote { get; set; }    
}

public class BaseFooterFavoritedViewModel
{
    public bool IsFavorited { get; set; }
    public int FavoritedCount { get; set; }
}

public class BaseFooterRateFavoritedViewModel : BaseFooterFavoritedViewModel
{
    public VoteType Vote { get; set; }
}