
public class LandOnTile : Event
{
    private Data data;
    private void Start(Delegator delegator, int playerNumber)
    {
        delegator.landOnTile = this.CheckTile;
    }
    private void CheckTile(Delegator delegator, int playerNumber)
    {
        int playerPosition = data.PlayerPositions[playerNumber];
        Tile tilePlayerLanded = data.GetTiles()[playerPosition];
        
        if (tilePlayerLanded is Property)
        {

            SetNextEvent(delegator, EventType.LandOnProperty);
        }
        else if (tilePlayerLanded is Chance)
        {

            SetNextEvent(delegator, EventType.LandOnCardTile);
        }
        else if (tilePlayerLanded is GoToJail)
        {

            SetNextEvent(delegator, EventType.GoToJail);        
        }
        else if (tilePlayerLanded is TaxTile)
        {

            SetNextEvent(delegator, EventType.PayTax);
        }
        else
        {
            SetNextEvent(delegator, EventType.CheckExtraTurn);
        }
    }

    protected override void SetNextEvent(Delegator delegator, EventType nextEvent)
    {
        delegator.nextEvent = nextEvent;
        delegator.landOnTile = this.Start;
    }

}
