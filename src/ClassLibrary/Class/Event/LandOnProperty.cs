public class LandOnProperty : Event
{

    private int playerPosition;
    private Property? currentProperty;

    public LandOnProperty(Delegator delegator) : base(delegator)
    {
        this.delegator= delegator;
        delegator.landOnProperty = this.Start;
        
    }

    int playerNumber => this.delegator!.CurrentPlayerNumber;

    private void Start(Bank bank, Board board, TileManager tileManager)
    {
        this.delegator!.landOnProperty = this.CheckOwner;
        this.playerPosition = board.PlayerPositions[playerNumber];
        this.currentProperty = (Property) tileManager.Tiles[playerPosition];
    }
    private void CheckOwner(Bank bank, Board board, TileManager tileManager)
    {
        if (this.currentProperty!.OwnerPlayerNumber == playerNumber)
        {
            this.SetNextEvent(EventType.CheckExtraTurn);
        }
        else if (currentProperty == null)
        {
            this.delegator!.boolDecisionType = BoolDecisionType.WantToBuyProperty;
            this.delegator.landOnProperty = this.WantPlayerBuyProperty;
        }
    }
    private void WantPlayerBuyProperty(Bank bank, Board board, TileManager tileManager)
    {
        if (this.delegator!.PlayerBoolDecision)
        {
            int propertyPrice = this.currentProperty!.Price;
            tileManager.ChangePropertyOwner(playerPosition, playerNumber);
            bank.ChangeBalance(playerNumber, -propertyPrice);
        }

        this.SetNextEvent(EventType.CheckExtraTurn);
    }

    protected override void SetNextEvent(EventType nextEvent)
    {
        this.delegator!.nextEvent = nextEvent;
        delegator.landOnProperty = this.Start;
    }
}
