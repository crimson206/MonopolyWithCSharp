
public abstract class Event
{
    protected DataCenter dataCenter;
    protected Delegator delegator;
    protected BoolCopier boolCopier;

    protected int playerNumber => this.delegator.CurrentPlayerNumber;
    protected bool playerBoolDecision => this.delegator.PlayerBoolDecision;
    protected int[] playerRollDiceResult => this.delegator.PlayerRollDiceResult;
    protected string recommendedString { set => this.delegator.RecommendedString = value; }
    protected Delegator.DelPlayerEvent nextAutoEvent { set => this.delegator.PlayerNextEvent = value; }
    protected Delegator.DelPlayerDecision nextDecision { set => this.delegator.PlayerNextDecision = value; }


    protected BankData bankData => this.dataCenter.Bank;
    protected BoardData boardData => this.dataCenter.Board;
    protected DoubleSideEffectData doubleSideEffectData => this.dataCenter.DoubleSideEffect;
    protected JailData jailData => this.dataCenter.Jail;

    public Event(Event proviousEvent)
    {
        this.delegator = proviousEvent.delegator;
        this.dataCenter = proviousEvent.dataCenter;
        this.boolCopier = proviousEvent.boolCopier;
    }

    /// event

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
