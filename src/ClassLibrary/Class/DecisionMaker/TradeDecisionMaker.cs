public class TradeDecisionMaker : DecisionMaker, ITradeDecisionMaker
{
    public TradeDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
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