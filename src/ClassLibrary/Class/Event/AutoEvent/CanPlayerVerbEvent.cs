public class CanPlayerVerbEvent : AutoEvent
{
    private string doYouWantTo => String.Format("Does Player{0} want to ", this.playerNumber);
    public CanPlayerVerbEvent(Event previousEvent) : base(previousEvent)
    {

    }

    public void CanPlayerUseJailFreeCard()
    {
        int turnsInJailCount = this.jailData.TurnsInJailCounts[playerNumber];

        if (this.CopyConditionBool(turnsInJailCount != 0))
        {
            this.recommendedString = this.doYouWantTo + "use a jailfree card?";
        }
        else
        {
            this.nextDecision = this.wantPlayerUseJailFreeCard.MakeDecision;
        }
    }
}