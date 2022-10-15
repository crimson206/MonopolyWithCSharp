
public abstract class PlayerEvent : Event
{
    public PlayerEvent(Event previousEvent) : base(previousEvent)
    {

    }

    public PlayerEvent(Delegator delegator, DataCenter dataCenter, BoolCopier boolCopier) : base( delegator, dataCenter, boolCopier)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;
        this.boolCopier = boolCopier;
    }

    protected bool CopyConditionBool(bool conditionBool) => this.boolCopier.CopyConditionBool(conditionBool);
    public abstract void RunEvent();

}
