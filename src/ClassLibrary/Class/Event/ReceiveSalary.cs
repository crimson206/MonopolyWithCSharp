
    public class ReceiveSalary : Event
    {
        private Data data;
        public void Start(Delegator delegator, int playerNumber, Bank bank)
        {
            delegator.receiveSalary = this.ReceivePlayerSalary;
        }
        public void ReceivePlayerSalary(Delegator delegator, int playerNumber, Bank bank)
        {
            bank.ChangeBalance(playerNumber, bank.Salary);
            this.SetNextEvent(delegator, EventType.LandOnTile);
        }
        protected override void SetNextEvent(Delegator delegator, EventType nextEvent)
        {
            delegator.nextEvent = nextEvent;
            delegator.receiveSalary = this.Start;
        }
    }
