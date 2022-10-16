
public abstract class Event
{
    protected DataCenter dataCenter;
    protected Delegator delegator;
    protected BoolCopier boolCopier;
    protected EventFlow eventFlow;
    protected HandlerDistrubutor handlerDistrubutor;
    protected Random random = new Random();
    protected EventStoragy events => new EventStoragy(this);
    protected Delegator.DelEvent newEvent { set => this.delegator.NextEvent = value; }

    /// shortcuts
    protected int playerNumber => this.eventFlow.CurrentPlayerNumber;
    protected int playerPosition => this.boardData.PlayerPositions[this.playerNumber];
    protected string stringPlayer => String.Format("Player{0}", this.playerNumber);

    protected bool CopyConditionBool(bool conditionBool) => this.boolCopier.CopyConditionBool(conditionBool);
    protected bool CopydecisionBool(bool decisionBool) => this.boolCopier.CopyDecisionBool(decisionBool);
    protected BankData bankData => this.dataCenter.Bank;
    protected BoardData boardData => this.dataCenter.Board;
    protected DoubleSideEffectData doubleSideEffectData => this.dataCenter.DoubleSideEffect;
    protected JailHandlerData jailData => this.dataCenter.Jail;
    protected List<TileData> tileDatas => this.dataCenter.TileDatas;

    public Event(DataCenter dataCenter, Delegator delegator, BoolCopier boolCopier, EventFlow eventFlow, HandlerDistrubutor handlerDistrubutor)
    {
        this.dataCenter = dataCenter;
        this.delegator = delegator;
        this.boolCopier = boolCopier;
        this.eventFlow = eventFlow;
        this.handlerDistrubutor = handlerDistrubutor;
    }

    protected Event(Event previousEvent)
    {
        this.dataCenter = previousEvent.dataCenter;
        this.delegator = previousEvent.delegator;
        this.eventFlow = previousEvent.eventFlow;
        this.boolCopier = previousEvent.boolCopier;
        this.handlerDistrubutor = previousEvent.handlerDistrubutor;
    }
    protected void InheritInfo(Event previousEvent)
    {
        this.dataCenter = previousEvent.dataCenter;
        this.delegator = previousEvent.delegator;
        this.eventFlow = previousEvent.eventFlow;
        this.boolCopier = previousEvent.boolCopier;
        this.handlerDistrubutor = previousEvent.handlerDistrubutor;
    }
}
