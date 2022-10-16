public class IndependentEvent : Event
{
    public IndependentEvent(Event previousEvent) : base(previousEvent)
    {
        
    }

    public void StartTurn()
    {
        if (this.jailData.TurnsInJailCounts[playerNumber] != 0)
        {
            throw new NotImplementedException();
        }
        else
        {
            this.nextEvent += this.RollDice;
            this.nextEvent += this.events.BoardEvent.Move;
            if (this.CopyConditionBool(this.boardData.PlayerPassedGo[playerNumber]))
            {
                this.nextEvent += this.events.BankEvent.ReceiveSalary;
            }
            this.nextEvent += this.events.TileEvent.LandOnTile;
        }
    }

    public void RollDice()
    {
        this.eventFlow.RoolDiceResult = Dice.Roll(this.random);
        this.eventFlow.RecommentedString = String.Format("Player{0} rolled" );
    }

}
