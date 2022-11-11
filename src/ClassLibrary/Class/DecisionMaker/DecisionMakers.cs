public class DecisionMakers
{
    private IAuctionDecisionMaker auctionDecisionMaker = new AuctionDecisionMaker();
    private IPropertyPurchaseDecisionMaker propertyPurchaseDecisionMaker = new PropertyPurchaseDecisionMaker();
    private IJailFreeCardUsageDecisionMaker jailFreeCardUsageDecisionMaker = new JailFreeCardUsageDecisionMaker();
    private IJailFinePaymentDecisionMaker jailFinePaymentDecisionMaker = new JailFinePaymentDecisionMaker();

    public IAuctionDecisionMaker AuctionDecisionMaker {get => this.auctionDecisionMaker; set => this.auctionDecisionMaker = value; }
    public IPropertyPurchaseDecisionMaker PropertyPurchaseDecisionMaker {get => this.propertyPurchaseDecisionMaker; set => this.propertyPurchaseDecisionMaker = value; }
    public IJailFreeCardUsageDecisionMaker JailFreeCardUsageDecisionMaker {get => this.jailFreeCardUsageDecisionMaker; set => this.jailFreeCardUsageDecisionMaker = value; }
    public IJailFinePaymentDecisionMaker JailFinePaymentDecisionMaker { get => this.jailFinePaymentDecisionMaker; set => this.jailFinePaymentDecisionMaker = value; }
}