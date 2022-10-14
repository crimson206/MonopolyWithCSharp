public class LandOnProperty : Event
{


    private int playerPosition;
    private Property? currentProperty;
    private BoardHandler board;
    private TileManager tileManager;
    private BankHandler bank;
    public LandOnProperty(EventStorage eventStorage, Delegator delegator, BankHandler bank, BoardHandler board, TileManager tileManager) : base(eventStorage, delegator)
    {
        this.events = eventStorage;
        this.bank = bank;
        this.board = board;
        this.tileManager = tileManager;
        this.delegator= delegator;
        delegator.NextEvent = this.Start;

    }

    int playerNumber => this.delegator!.CurrentPlayerNumber;

    public override void Start()
    {
        this.delegator!.NextEvent = this.CheckOwner;
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
            this.delegator.NextEvent = this.WantPlayerBuyProperty;
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

        delegator.NextEvent = gameEvent.Start;
    }
}
