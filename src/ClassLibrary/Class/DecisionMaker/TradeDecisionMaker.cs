public class TradeDecisionMaker : ITradeDecisionMaker
{
    private List<IPropertyData> propertyDatas = new List<IPropertyData>();

    public int SelectTradeTarget(int playerNumber)
    {
        if (playerNumber == 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public IPropertyData SelectPropertyToGet(int playerNumber)
    {
        return this.propertyDatas[0];
    }

    public IPropertyData SelectPropertyToGive(int playerNumber)
    {
        return this.propertyDatas[0];
    }

    public int DecideAdditionalMoney(int playerNumber)
    {
        return 0;
    }

    public bool MakeTradeTargetDecisionOnTradeAgreement(int playerNumber)
    {
        return true;
    }
}