public class TradeDecisionMaker : ITradeDecisionMaker
{
    private List<IPropertyData> propertyDatas = new List<IPropertyData>();
    private IDataCenter dataCenter;
    public TradeDecisionMaker(IDataCenter dataCenter)
    {
        this.dataCenter = dataCenter;
    }

    public int SelectTradeTarget(int playerNumber)
    {
        return this.dataCenter.TradeHandler.SelectableTargetNumbers[0];
    }

    public IPropertyData SelectPropertyToGet(int playerNumber)
    {
        return this.dataCenter.TradeHandler.TradablePropertiesOfTradeTarget[0];
    }

    public IPropertyData SelectPropertyToGive(int playerNumber)
    {
        return this.dataCenter.TradeHandler.TradablePropertiesOfTradeOwner[0];
    }

    public int DecideAdditionalMoney(int playerNumber)
    {
        return 50;
    }

    public bool MakeTradeTargetDecisionOnTradeAgreement(int playerNumber)
    {
        return true;
    }
}