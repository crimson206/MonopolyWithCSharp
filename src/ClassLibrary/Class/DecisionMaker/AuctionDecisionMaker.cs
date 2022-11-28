public class AuctionDecisionMaker : PropertyDecisionMaker, IAuctionDecisionMaker
{

    public AuctionDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }
    int Participant => this.dataCenter.AuctionHandler.NextParticipantNumber;
    IPropertyData PropertyToAuction => this.dataCenter.AuctionHandler.PropertyToAuction!;
    int MaxPrice => this.dataCenter.AuctionHandler.MaxPrice;

    public int SuggestPrice()
    {
        double originalPrice = (double)this.PropertyToAuction.Price;
        double factor1 = this.propertyValueMeasurer.ConsiderPriceAndMonopolyWhenGettingAProperty(this.Participant, this.PropertyToAuction);
        double factor2 = this.ConsiderBalanceCostAndEnemiesRents(this.Participant, (int)(factor1 * originalPrice));


        int priceInMind = (int)(originalPrice * factor1 * factor2);

        int decision = (priceInMind >= this.MaxPrice + 20? this.MaxPrice + 20 : 0);

        return decision;
    }
}
