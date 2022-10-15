
public class PlayerStartTurn : PlayerEvent
{
    public PlayerStartTurn(Event previousEvent) : base(previousEvent)
    {

    }

    public PlayerStartTurn(Delegator delegator, DataCenter dataCenter, BoolCopier boolCopier) : base( delegator, dataCenter, boolCopier)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;
        this.boolCopier = boolCopier;
    }

    public override void RunEvent()
    {
        this.delegator.RecommendedString = String.Format("Player{0} starts a new turn", playerNumber);
        int turnsInJailCount = this.jailHandler.TurnsInJailCounts[playerNumber];

        if (this.CopyConditionBool(turnsInJailCount != 0))
        {
            this.playerNextEvent = this.canPlayerUseFreeJailCard.RunEvent;
        }
        else
        {
            this.playerNextEvent = this.canPlayerPayJailFine.RunEvent;
        }
    }



}
