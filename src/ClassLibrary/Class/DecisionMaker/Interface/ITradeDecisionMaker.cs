public interface ITradeDecisionMaker
{
    public int? SelectTradeTarget();

    public int? SelectPropertyToGet();

    public int? SelectPropertyToGive();

    public int DecideAdditionalMoney();

    public bool MakeTradeTargetDecisionOnTradeAgreement();
}