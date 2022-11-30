public class DecisionMakers : IDecisionMakers
{
    private IAuctionDecisionMaker auctionDecisionMaker;
    private IPropertyPurchaseDecisionMaker propertyPurchaseDecisionMaker;
    private IJailFreeCardUsageDecisionMaker jailFreeCardUsageDecisionMaker; 
    private IJailFinePaymentDecisionMaker jailFinePaymentDecisionMaker;
    private ITradeDecisionMaker tradeDecisionMaker;
    private IHouseBuildDecisionMaker houseBuildDecisionMaker;
    private ISellItemDecisionMaker sellItemDecisionMaker;
    private IUnmortgageDecisionMaker unmortgageDecisionMaker;

    public DecisionMakers(IDataCenter dataCenter)
    {
        this.tradeDecisionMaker = new TradeDecisionMaker(dataCenter);
        this.houseBuildDecisionMaker = new HouseBuildDecisionMaker(dataCenter);
        this.sellItemDecisionMaker = new SellItemDecisionMaker(dataCenter);
        this.auctionDecisionMaker = new AuctionDecisionMaker(dataCenter);
        this.jailFinePaymentDecisionMaker = new JailFinePaymentDecisionMaker(dataCenter);
        this.propertyPurchaseDecisionMaker = new PropertyPurchaseDecisionMaker(dataCenter);
        this.jailFreeCardUsageDecisionMaker = new JailFreeCardUsageDecisionMaker(dataCenter);
        this.unmortgageDecisionMaker = new DemortgageDecisionMaker(dataCenter);
    }

    public IAuctionDecisionMaker AuctionDecisionMaker {get => this.auctionDecisionMaker; set => this.auctionDecisionMaker = value; }
    public IPropertyPurchaseDecisionMaker PropertyPurchaseDecisionMaker {get => this.propertyPurchaseDecisionMaker; set => this.propertyPurchaseDecisionMaker = value; }
    public IJailFreeCardUsageDecisionMaker JailFreeCardUsageDecisionMaker {get => this.jailFreeCardUsageDecisionMaker; set => this.jailFreeCardUsageDecisionMaker = value; }
    public IJailFinePaymentDecisionMaker JailFinePaymentDecisionMaker { get => this.jailFinePaymentDecisionMaker; set => this.jailFinePaymentDecisionMaker = value; }
    public ITradeDecisionMaker TradeDecisionMaker { get => this.tradeDecisionMaker; set => this.tradeDecisionMaker = value; }
    public IHouseBuildDecisionMaker HouseBuildDecisionMaker { get => this.houseBuildDecisionMaker; set => this.houseBuildDecisionMaker = value; }
    public ISellItemDecisionMaker SellItemDecisionMaker { get => this.sellItemDecisionMaker; }
    public IUnmortgageDecisionMaker DemortgageDecisionMaker { get => this.unmortgageDecisionMaker; }
}