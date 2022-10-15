public abstract class DecisionEvent : Event
{

    public DecisionEvent(Event previousEvent) : base(previousEvent)
    {

    }

    protected bool CopyDecisionBool(bool conditionBool) => this.boolCopier.CopyDecisionBool(conditionBool);

    protected enum Setting
    {
        Manual,
        Default
    }
    
    protected List<Enum> playerSettings = new List<Enum> {Setting.Default, Setting.Manual, Setting.Manual, Setting.Manual };

    public abstract void MakeDecision();

    protected bool MakeDecisionManually()
    {
        return this.delegator.ManualDecision!();
    }
}
