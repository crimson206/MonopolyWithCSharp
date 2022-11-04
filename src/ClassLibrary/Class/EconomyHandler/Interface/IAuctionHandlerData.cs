public interface IAuctionHandlerData
{
    public bool IsAuctionOn { get; }

    public List<int> SuggestedPrices { get; }

    public int? FinalPrice { get; }

    public int? WinnerNumber { get; }
}