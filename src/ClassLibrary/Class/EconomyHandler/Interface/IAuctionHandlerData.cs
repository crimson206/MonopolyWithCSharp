public interface IAuctionHandlerData
{
    public bool IsAuctionOn { get; }

    public Dictionary<int, int> SuggestedPrices { get; }

    public int? FinalPrice { get; }

    public int? WinnerNumber { get; }

    public int NextParticipantNumber { get; }
}