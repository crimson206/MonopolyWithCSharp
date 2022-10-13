public class WantToUseJailFreeCard : DecisionMaker
{
    protected new enum Setting
    {
        Manual,
    }

    public WantToUseJailFreeCard(Delegator delegator) : base(delegator)
    {

    }

    protected override void MakeDecisionManually()
    {
        this.delegator.manualDecision!();
    }

}
