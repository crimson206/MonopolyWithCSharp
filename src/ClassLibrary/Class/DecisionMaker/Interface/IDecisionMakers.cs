public interface IDecisionMakers
{
    public IAuctionDecisionMaker AuctionDecisionMaker { get; }
    public IPropertyPurchaseDecisionMaker PropertyPurchaseDecisionMaker { get; }
    public IJailFreeCardUsageDecisionMaker JailFreeCardUsageDecisionMaker { get; }
    public IJailFinePaymentDecisionMaker JailFinePaymentDecisionMaker { get; }
    public ITradeDecisionMaker TradeDecisionMaker{ get; }
    public IHouseBuildDecisionMaker HouseBuildDecisionMaker { get; }
    public ISellItemDecisionMaker SellItemDecisionMaker { get; }
    public IUnmortgageDecisionMaker DemortgageDecisionMaker { get; }
}