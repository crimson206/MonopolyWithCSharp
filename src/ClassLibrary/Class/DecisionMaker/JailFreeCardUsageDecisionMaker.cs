public class JailFreeCardUsageDecisionMaker : DecisionMaker, IJailFreeCardUsageDecisionMaker
{

    public JailFreeCardUsageDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }

    public bool MakeDecisionOnUsage(int playerNumber)
    {
        return true;
    }
}