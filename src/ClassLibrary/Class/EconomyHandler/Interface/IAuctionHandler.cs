public interface IAuctionHandler : IAuctionHandlerData
{
    public void SetAuctionCondition(List<int> participantNumbers, int initialPrice, IPropertyData propertyToAuction, bool firstParticipantIsForcedToBid);
    public void BidNewPriceInTurn(int newPrice);

}