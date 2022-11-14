public class TradeDecisionMaker : ITradeDecisionMaker
{
    private List<IPropertyData> propertyDatas = new List<IPropertyData>();
    private IDataCenter dataCenter;
    public TradeDecisionMaker(IDataCenter dataCenter)
    {
        this.dataCenter = dataCenter;
    }

    public int SelectTradeTarget()
    {
        return 0;
    }

    public int? SelectPropertyToGet()
    {
        return 0;
    }

    public int? SelectPropertyToGive()
    {
        return 0;
    }

    public int DecideAdditionalMoney()
    {
        return 50;
    }

    public bool MakeTradeTargetDecisionOnTradeAgreement()
    {
        return true;
    }
}