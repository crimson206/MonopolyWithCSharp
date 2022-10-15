public class WantPlayerUseJailFreeCard : DecisionEvent
{
    private bool decision;
    public WantPlayerUseJailFreeCard(Event previousEvent) : base(previousEvent)
    {

    }

    public override void MakeDecision()
    {
        switch (playerSettings[playerNumber])
        {
            case Setting.Manual:
                this.decision = this.MakeDecisionManually();
                break;
            default:
                this.decision = true;
                break;
        }

        if ( this.CopyDecisionBool(this.decision) )
        {
            throw new Exception();
        }
        else
        {
            throw new Exception();
        }
    }


}
