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
        double value = this.CalculateValueConsideringAllWhenGettingAProperty(this.player, this.CurrentProperty);

        bool output = (value >= 1? true : false);

        return output;
    }



    private IPropertyData GetCurrentProperty()
    {
        int position = this.dataCenter.Board.PlayerPositions[this.player];
        IPropertyData currentProperty = (IPropertyData)this.dataCenter.TileDatas[position];

        return currentProperty;
    }
}
