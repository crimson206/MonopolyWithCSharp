public class PropertyPurchaseDecisionMaker : PropertyDecisionMaker, IPropertyPurchaseDecisionMaker
{

    public PropertyPurchaseDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }
    int player => this.dataCenter.EventFlow.CurrentPlayerNumber;
    IPropertyData CurrentProperty => this.GetCurrentProperty();

    public bool MakeDecisionOnPurchase()
    {
        int price = this.CurrentProperty.Price;
        double factor1 = this.propertyValueMeasurer.ConsiderPriceAndMonopolyWhenGettingAProperty(this.player, this.CurrentProperty);
        double factor2 = this.ConsiderBalanceCostAndEnemiesRents(this.player, (int)(factor1 * price));

        double decisionFactor = factor1 * factor2;

        bool output = (decisionFactor >= 1? true : false);

        return output;
    }

    private IPropertyData GetCurrentProperty()
    {
        int position = this.dataCenter.Board.PlayerPositions[this.player];
        IPropertyData currentProperty = (IPropertyData)this.dataCenter.TileDatas[position];

        return currentProperty;
    }
}
