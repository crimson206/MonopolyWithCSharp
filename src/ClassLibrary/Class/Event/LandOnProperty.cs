public class LandOnProperty : Event
{


    private int playerPosition;
    private Property? currentProperty;
    private Board board;
    private TileManager tileManager;
    private Bank bank;
    public LandOnProperty(EventStorage eventStorage, Delegator delegator, Bank bank, Board board, TileManager tileManager) : base(eventStorage, delegator)
    {
        this.events = eventStorage;
        this.bank = bank;
        this.board = board;
        this.tileManager = tileManager;
        this.delegator= delegator;
        delegator.nextEvent = this.Start;

    }

    int playerNumber => this.delegator!.CurrentPlayerNumber;

    public override void Start()
    {
        this.delegator!.nextEvent = this.CheckOwner;
        this.playerPosition = board.PlayerPositions[playerNumber];
        this.currentProperty = (Property) tileManager.Tiles[playerPosition];
    }
    private void CheckOwner()
    {
        if (this.currentProperty!.OwnerPlayerNumber == playerNumber)
        {
            /// this.SetNextEvent(EventType.CheckExtraTurn);
        }
        else if (currentProperty == null)
        {
            ///this.delegator!.boolDecisionType = BoolDecisionType.WantToBuyProperty;
            this.delegator.nextEvent = this.WantPlayerBuyProperty;
        }
    }
    private void WantPlayerBuyProperty()
    {
        if (this.delegator!.BoolDecision == true)
        {
            

            int propertyPrice = this.currentProperty!.Price;
            tileManager.propertyManager.ChangeOwner(currentProperty, playerNumber);
            bank.Balances[playerNumber] -= propertyPrice;
        }

        /// must be check double turn
        /// ////this.SetNextEvent(eventStorage.rollToMove);
    }

    protected override void SetNextEvent(Event gameEvent)
    {

        delegator.nextEvent = gameEvent.Start;
    }
}
