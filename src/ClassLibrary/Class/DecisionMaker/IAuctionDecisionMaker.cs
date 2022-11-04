public interface IAuctionDecisionMaker
{
    public int SuggestAuctionPrice(int playerNumber);
    public int SetSuggestedPrices(List<int> suggestedPrices);
}