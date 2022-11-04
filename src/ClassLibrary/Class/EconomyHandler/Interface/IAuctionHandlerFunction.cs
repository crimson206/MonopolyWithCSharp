public interface IAuctionHandlerFunction
{
    public void SetAuctionCondition(List<int> participantNumbers, int initialPrice);
    public void SuggestNewPriceInTurn(int newPrice);
    public void SetAuctionResultAndCloseAuction();
}