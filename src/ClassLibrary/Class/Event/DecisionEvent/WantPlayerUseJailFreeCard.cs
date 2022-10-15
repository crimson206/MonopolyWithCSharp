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
                decision = this.MakeDecisionManually();
                break;
            default:
                decision = true;
                break;
        }

        if ( this.CopyDecisionBool(decision))
        {
            throw new Exception();
        }
        else
        {
            throw new Exception();
        }
    }


}
