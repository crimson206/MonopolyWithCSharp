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
        bool result = this.delegator.manualDecision!();
        if (result is true)
        {
            delegator.RecommendedString =
            String.Format("Player{0} paid the jail fine", this.playerNumber);
        }
        else
        {
            delegator.RecommendedString =
            String.Format("Player{0} refused to pay the jail fine", this.playerNumber);
        }
    }

}
