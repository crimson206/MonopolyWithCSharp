public class DecisionMakers : IDecisionMakers
{
    private IAuctionDecisionMaker auctionDecisionMaker = new AuctionDecisionMaker();
    private IPropertyPurchaseDecisionMaker propertyPurchaseDecisionMaker = new PropertyPurchaseDecisionMaker();
    private IJailFreeCardUsageDecisionMaker jailFreeCardUsageDecisionMaker = new JailFreeCardUsageDecisionMaker();
    private IJailFinePaymentDecisionMaker jailFinePaymentDecisionMaker = new JailFinePaymentDecisionMaker();
    private ITradeDecisionMaker tradeDecisionMaker;

    public DecisionMakers(IDataCenter dataCenter)
    {
        this.tradeDecisionMaker = new TradeDecisionMaker(dataCenter);
    }

    public IAuctionDecisionMaker AuctionDecisionMaker {get => this.auctionDecisionMaker; set => this.auctionDecisionMaker = value; }
    public IPropertyPurchaseDecisionMaker PropertyPurchaseDecisionMaker {get => this.propertyPurchaseDecisionMaker; set => this.propertyPurchaseDecisionMaker = value; }
    public IJailFreeCardUsageDecisionMaker JailFreeCardUsageDecisionMaker {get => this.jailFreeCardUsageDecisionMaker; set => this.jailFreeCardUsageDecisionMaker = value; }
    public IJailFinePaymentDecisionMaker JailFinePaymentDecisionMaker { get => this.jailFinePaymentDecisionMaker; set => this.jailFinePaymentDecisionMaker = value; }
    public ITradeDecisionMaker TradeDecisionMaker { get => this.tradeDecisionMaker; set => this.tradeDecisionMaker = value; }
}