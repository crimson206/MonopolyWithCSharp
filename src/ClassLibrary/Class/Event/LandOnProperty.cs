public class LandOnProperty : Event
{
    private Data data;
    private void Start(Delegator delegator, int playerNumber, Bank bank, TileManager tileManager)
    {
        delegator.landOnProperty = this.CheckOwner;
    }
    private void CheckOwner(Delegator delegator, int playerNumber, Bank bank, TileManager tileManager)
    {
        int playerPosition = data.PlayerPositions[playerNumber];
        Property currentProperty = (Property) data.GetTiles()[playerNumber];

        if (currentProperty.OwnerPlayerNumber == playerNumber)
        {
            this.SetNextEvent(delegator, EventType.CheckExtraTurn);
        }
        else if (currentProperty == null)
        {
            delegator.playerBoolDecision = BoolDecisionType.WantToBuyProperty;
            delegator.landOnProperty = this.WantPlayerBuyProperty;
        }
    }
    private void WantPlayerBuyProperty(Delegator delegator, int playerNumber, Bank bank, TileManager tileManager)
    {
        if (data.LastBoolDecisions[playerNumber])
        {
            int playerPosition = data.PlayerPositions[playerNumber];
            tileManager.ChangePropertyOwner(playerPosition, playerNumber);
        }

        this.SetNextEvent(delegator, EventType.CheckExtraTurn);
    }

    protected override void SetNextEvent(Delegator delegator, EventType nextEvent)
    {
        delegator.nextEvent = nextEvent;
        delegator.landOnProperty = this.Start;
    }
}
