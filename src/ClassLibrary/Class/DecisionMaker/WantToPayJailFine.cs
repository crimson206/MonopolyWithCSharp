public class WantToPayJailFine : DecisionMaker
{

    private new enum Setting
    {
        Manual,
    }

    public WantToPayJailFine(Delegator delegator) : base(delegator)
    {

    }

    protected override void MakeDecisionManually()
    {
        bool result = this.delegator.ManualDecision!();

    }

}
