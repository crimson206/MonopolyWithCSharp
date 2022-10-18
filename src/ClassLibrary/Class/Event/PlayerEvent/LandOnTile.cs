
public class LandOnTile : Event
{
    private Tile currentTile => this.GetCurrentTile();

    private TileManager tileManager => tileManagerTaker.tileManager!;
    private TileManagerTaker tileManagerTaker;
    private BankHandler bankHandler => bankHandlerTaker.bankHandler!;
    private BankHandlerTaker bankHandlerTaker;
    private BoardHandler boardHandler => boardHandlerTaker.boardHandler!;
    private BoardHandlerTaker boardHandlerTaker;
    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;

        
    public LandOnTile(Event previousEvent) : base(previousEvent)
    {
        this.tileManagerTaker = new TileManagerTaker(this.handlerDistrubutor);
        this.bankHandlerTaker = new BankHandlerTaker(this.handlerDistrubutor);
        this.boardHandlerTaker = new BoardHandlerTaker(this.handlerDistrubutor);
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
    }

    public void ResetAndAddEvent()
    {
        if (CopyConditionBool(this.currentTile is Property))
        {
            Property currentProperty = (Property) this.currentTile;
            if (this.CopyConditionBool(currentProperty.OwnerPlayerNumber == this.playerNumber))
            {
                this.eventFlow.RecommentedString = this.stringPlayer + " landed on his/her property";

            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
            }
            else if (this.CopyConditionBool(currentProperty.OwnerPlayerNumber == null))
            {
                bool canBuyProperty = currentProperty.Price <= this.bankHandler.Balances[this.playerNumber];
                if (this.CopyConditionBool( canBuyProperty ))
                {
                this.eventFlow.RecommentedString = this.stringPlayer + " landed on a free property";

                this.delegator.ResetAndAddFollowingEvent = this.events.WantBuyProperty.ResetAndAddEvent;
                }
                else
                {
                    this.eventFlow.RecommentedString = this.stringPlayer + " landed on a free property, but has no enough money to buy it";

            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
                }
            }
            else
            {
                int propertyOwner = (int) currentProperty.OwnerPlayerNumber!;
                
                int rentOfProperty = currentProperty.CurrentRent;
                this.bankHandler.TransferBalanceFromTo(this.playerNumber, propertyOwner, rentOfProperty);
                this.eventFlow.RecommentedString = this.stringPlayer + " paid a rent to the owner of the property";

            this.delegator.ResetAndAddFollowingEvent = this.events.CheckBankrupt.IfTrue_ResetAndAddEvent;
            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
            }
            
        }
        else if ( CopyConditionBool(this.currentTile is LuxuryTax))
        {
            LuxuryTax luxuryTax = (LuxuryTax) this.currentTile;
            this.bankHandler.DecreaseBalance(this.playerNumber, luxuryTax.Tax);
            this.eventFlow.RecommentedString = this.stringPlayer + " paid the luxury tax";

            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
        }
        else if ( CopyConditionBool(this.currentTile is IncomeTax))
        {
            IncomeTax incomeTax = (IncomeTax) this.currentTile;
            this.bankHandler.DecreaseBalance(this.playerNumber, incomeTax.Tax);
            this.eventFlow.RecommentedString = this.stringPlayer + " paid the income tax";

            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
        }
        else if ( CopyConditionBool(this.currentTile is GoToJail))
        {
            this.eventFlow.RecommentedString = this.stringPlayer + " landed on GoToJail. ";

            this.delegator.ResetAndAddFollowingEvent = this.events.HasJailPenalty.ResetAndAddEvent;
        }
        else
        {
            this.delegator.ResetAndAddFollowingEvent = this.events.CheckAndDoExtraTurn.ResetAndAddEvent;
        }

    }

    public Tile GetCurrentTile()
    {
        int playerPosition = this.boardHandler.PlayerPositions[this.playerNumber];
        return this.tileManager.Tiles[playerPosition];
    } 

}
