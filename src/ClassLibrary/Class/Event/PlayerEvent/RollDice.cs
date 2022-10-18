public class RollDice : Event
{
    private string stringRollDiceResult => this.ConvertRollDiceResultToString();
    public RollDice(Event previousEvent) : base(previousEvent)
    {
    }

    public void Pass()
    {
        this.eventFlow.RollDiceResult = Dice.Roll(this.random);

        this.eventFlow.RecommentedString = stringPlayer + " rolled " + this.ConvertRollDiceResultToString();
    }

    protected string ConvertRollDiceResultToString()
    {
        return String.Join(", ", from dieValue in this.eventFlow.RollDiceResult select dieValue.ToString());
    }
}
