public class CanPlayerUseFreeJailCard : PlayerEvent
{


    public CanPlayerUseFreeJailCard(Event previousEvent) : base(previousEvent)
    {

    }

    public override void RunEvent()
    {
        int freeJailCardCount = this.jailHandler.FreeJailCardCounts[this.playerNumber];

        if ( this.CopyConditionBool(freeJailCardCount != 0) )
        {
            this.recommendedString = String.Format("Player{0} has {1} freejail cards. Want Player{0} use a card?", playerNumber, freeJailCardCount);

            this.playerNextDecision = this.wantPlayerUseJailFreeCard.MakeDecision;
        }
        else
        {
            this.playerNextEvent = this.canPlayerPayJailFine.RunEvent;
        }
    }
}
