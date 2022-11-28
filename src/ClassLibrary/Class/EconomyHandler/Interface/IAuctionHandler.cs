public interface IAuctionHandler : IAuctionHandlerData
{
    public void SetAuctionCondition(List<int> participantNumbers, int initialPrice, IPropertyData propertyToAuction);
    public void SuggestNewPriceInTurn(int newPrice);

}