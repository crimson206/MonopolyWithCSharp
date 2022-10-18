public class StayInJail : Event
{

    private JailHandler jailHandler => jailHandlerTaker.jailHandler!;
    private JailHandlerTaker jailHandlerTaker;
    public StayInJail(Event previousEvent) : base(previousEvent)
    {
        this.jailHandlerTaker = new JailHandlerTaker(this.handlerDistrubutor);
    }

    public void Pass()
    {
        this.jailHandler.CountTurnInJail(this.playerNumber);
        this.eventFlow.RecommentedString = this.stringPlayer + " stays one more turn in jail";
    }
}
