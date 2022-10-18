
public abstract class Event
{
    protected Delegator delegator;
    protected BoolCopier boolCopier;
    protected EventFlow eventFlow;
    protected HandlerDistrubutor handlerDistrubutor;
    protected Random random = new Random();
    protected EventStoragy events => new EventStoragy(this);

    /// shortcuts
    protected int playerNumber => this.eventFlow.CurrentPlayerNumber;
    protected string stringPlayer => String.Format("Player {0}", this.playerNumber);


    protected bool CopyConditionBool(bool conditionBool) => this.boolCopier.CopyConditionBool(conditionBool);
    protected bool CopydecisionBool(bool decisionBool) => this.boolCopier.CopyDecisionBool(decisionBool);

    public Event(Delegator delegator, BoolCopier boolCopier, EventFlow eventFlow, HandlerDistrubutor handlerDistrubutor)
    {
        this.delegator = delegator;
        this.boolCopier = boolCopier;
        this.eventFlow = eventFlow;
        this.handlerDistrubutor = handlerDistrubutor;
    }

    protected Event(Event previousEvent)
    {
        this.delegator = previousEvent.delegator;
        this.eventFlow = previousEvent.eventFlow;
        this.boolCopier = previousEvent.boolCopier;
        this.handlerDistrubutor = previousEvent.handlerDistrubutor;
    }
    protected void InheritInfo(Event previousEvent)
    {
        this.delegator = previousEvent.delegator;
        this.eventFlow = previousEvent.eventFlow;
        this.boolCopier = previousEvent.boolCopier;
        this.handlerDistrubutor = previousEvent.handlerDistrubutor;
    }



}
