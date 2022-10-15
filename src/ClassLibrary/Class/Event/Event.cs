
public abstract class Event
{
    protected DataCenter dataCenter;
    protected Delegator delegator;
    protected BoolCopier boolCopier;

    protected int playerNumber => this.delegator.CurrentPlayerNumber;
    protected bool boolDecision => this.delegator.BoolDecision;
    protected int[] rollDiceResult => this.delegator.RollDiceResult;
    protected string recommendedString { set => this.delegator.RecommendedString = value; }
    protected Delegator.DelPlayerEvent playerNextEvent { set => this.delegator.PlayerNextEvent = value; }
    protected Delegator.DelPlayerDecision playerNextDecision { set => this.delegator.PlayerNextDecision = value; }


    protected BankHandler bankHandler => this.dataCenter.bankHandler;
    protected BoardHandler boardHandler => this.dataCenter.boardHandler;
    protected DoubleSideEffectHandler doubleSideEffectHandler => this.dataCenter.doubleSideEffectHandler;
    protected JailHandler jailHandler => this.dataCenter.jailHandler;

    public Event(Event proviousEvent)
    {
        this.delegator = proviousEvent.delegator;
        this.dataCenter = proviousEvent.dataCenter;
        this.boolCopier = proviousEvent.boolCopier;
    }

    /// events
    protected CanPlayerUseFreeJailCard canPlayerUseFreeJailCard => new CanPlayerUseFreeJailCard(this);
    protected PlayerRollToMove playerRollToMove => new PlayerRollToMove(this);
    protected CanPlayerPayJailFine canPlayerPayJailFine => new CanPlayerPayJailFine(this);

    /// decision
    protected WantPlayerUseJailFreeCard wantPlayerUseJailFreeCard => new WantPlayerUseJailFreeCard(this);

    public void SetEvent(DataCenter dataCenter, Delegator delegator, BoolCopier boolCopier)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;
        this.boolCopier = boolCopier;
    }
    public Event(Delegator delegator, DataCenter dataCenter, BoolCopier boolCopier)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;
        this.boolCopier = boolCopier;
    }

}
