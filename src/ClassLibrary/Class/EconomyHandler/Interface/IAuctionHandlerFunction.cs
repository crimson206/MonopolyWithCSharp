public interface IAuctionHandlerFunction
{
    public void SetAuctionCondition(List<bool> participants, int initialAuctionerNumber, int initialPrice);
    public void SuggestNewPriceInTurn(int newPrice);
}