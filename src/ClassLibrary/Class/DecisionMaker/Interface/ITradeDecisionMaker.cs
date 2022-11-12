public interface ITradeDecisionMaker
{
    public int SelectTradeTarget(int playerNumber);

    public IPropertyData SelectPropertyToGet(int playerNumber);

    public IPropertyData SelectPropertyToGive(int playerNumber);

    public int DecideAdditionalMoney(int playerNumber);

    public bool MakeTradeTargetDecisionOnTradeAgreement(int playerNumber);
}