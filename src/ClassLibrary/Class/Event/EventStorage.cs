
public class EventStoragy
{
    public Event previousEvent;
    public EventStoragy(Event previousEvent)
    {
        this.previousEvent = previousEvent;
    }
    public StartTurn StartTurn => new StartTurn(this.previousEvent);
    public WantUseJailFreeCard WantUseJailFreeCard => new WantUseJailFreeCard(this.previousEvent);
    public WantPayJailFine WantPayJailFine => new WantPayJailFine(this.previousEvent);
    public RollDice RollDice => new RollDice(this.previousEvent);
    public RolledDoubleToEscapeJail IfRolledDouble_EscapeJail => new RolledDoubleToEscapeJail(this.previousEvent);
    public MoveByRollDiceTotal MoveByRollDiceTotal => new MoveByRollDiceTotal(this.previousEvent);
    public Stayed3TurnsInJail Stayed3TurnsInJail => new Stayed3TurnsInJail(this.previousEvent);
    public StayInJail StayInJail => new StayInJail(this.previousEvent);
    public PassedGoToReceiveSalary PassedGoToReceiveSalary => new PassedGoToReceiveSalary(this.previousEvent);
    public LandOnTile LandOnTile => new LandOnTile(this.previousEvent);
    public CountRolledDouble CountRolledDouble => new CountRolledDouble(this.previousEvent);
    public EndTurn EndTurn => new EndTurn(this.previousEvent);
    public WantBuyProperty WantBuyProperty => new WantBuyProperty(this.previousEvent);
    public HasJailPenalty HasJailPenalty => new HasJailPenalty(this.previousEvent);
    public CheckRolledDouble3Times CheckRolledDouble3Times => new CheckRolledDouble3Times(this.previousEvent);
    public CheckAndDoExtraTurn CheckAndDoExtraTurn => new CheckAndDoExtraTurn(this.previousEvent);
    public CheckBankrupt CheckBankrupt => new CheckBankrupt(this.previousEvent);

}