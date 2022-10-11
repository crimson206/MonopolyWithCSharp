
public class ReceiveSalary : Event
{
    public ReceiveSalary(Delegator delegator) : base(delegator)
    {
        this.delegator= delegator;
        this.delegator.receiveSalary = this.Start;
    }
    int playerNumber => this.delegator!.CurrentPlayerNumber;

    public void Start(Bank bank)
    {
        this.delegator!.receiveSalary = this.ReceivePlayerSalary;
    }
    public void ReceivePlayerSalary(Bank bank)
    {
        bank.ChangeBalance(playerNumber, bank.Salary);
        this.SetNextEvent(EventType.LandOnTile);
    }
    protected override void SetNextEvent(EventType nextEvent)
    {
        this.delegator!.nextEvent = nextEvent;
        delegator.receiveSalary = this.Start;
    }
}
