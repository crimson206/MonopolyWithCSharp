
public class ReceiveSalary : Event
{
    private Bank bank;
    public ReceiveSalary(EventStorage eventStorage, Delegator delegator, Bank bank) : base(eventStorage, delegator)
    {
        this.bank = bank;
        this.delegator= delegator;
        this.delegator.nextEvent = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;

    public override void Start()
    {
        this.delegator!.nextEvent = this.ReceivePlayerSalary;
    }
    public void ReceivePlayerSalary()
    {
        bank.Balances[playerNumber] += bank.Salary;
        ///this.SetNextEvent(EventType.LandOnTile);
    }
    protected override void SetNextEvent(Event gameEvent)
    {
        delegator.nextEvent = gameEvent.Start;
    }
}
