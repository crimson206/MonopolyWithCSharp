public class DecisionMakers : IDecisionMakers
{
    private IAuctionDecisionMaker auctionDecisionMaker = new AuctionDecisionMaker();
    private IPropertyPurchaseDecisionMaker propertyPurchaseDecisionMaker = new PropertyPurchaseDecisionMaker();
    private IJailFreeCardUsageDecisionMaker jailFreeCardUsageDecisionMaker = new JailFreeCardUsageDecisionMaker();
    private IJailFinePaymentDecisionMaker jailFinePaymentDecisionMaker = new JailFinePaymentDecisionMaker();
    private ITradeDecisionMaker tradeDecisionMaker;
    private IHouseBuildDecisionMaker houseBuildDecisionMaker;

    public DecisionMakers(IDataCenter dataCenter)
    {
        this.tradeDecisionMaker = new TradeDecisionMaker(dataCenter);
        this.houseBuildDecisionMaker = new HouseBuildDecisionMaker(dataCenter);
    }

    public IAuctionDecisionMaker AuctionDecisionMaker {get => this.auctionDecisionMaker; set => this.auctionDecisionMaker = value; }
    public IPropertyPurchaseDecisionMaker PropertyPurchaseDecisionMaker {get => this.propertyPurchaseDecisionMaker; set => this.propertyPurchaseDecisionMaker = value; }
    public IJailFreeCardUsageDecisionMaker JailFreeCardUsageDecisionMaker {get => this.jailFreeCardUsageDecisionMaker; set => this.jailFreeCardUsageDecisionMaker = value; }
    public IJailFinePaymentDecisionMaker JailFinePaymentDecisionMaker { get => this.jailFinePaymentDecisionMaker; set => this.jailFinePaymentDecisionMaker = value; }
    public ITradeDecisionMaker TradeDecisionMaker { get => this.tradeDecisionMaker; set => this.tradeDecisionMaker = value; }
    public IHouseBuildDecisionMaker HouseBuildDecisionMaker { get => this.houseBuildDecisionMaker; set => this.houseBuildDecisionMaker = value; }
}