public class JailFinePaymentDecisionMaker : DecisionMaker, IJailFinePaymentDecisionMaker
{
    public JailFinePaymentDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }

    public bool MakeDecisionOnPayment(int playerNumber)
    {
        return true;
    }
}