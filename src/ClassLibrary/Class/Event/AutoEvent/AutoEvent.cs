
public abstract class AutoEvent : Event
{
    public AutoEvent(Event previousEvent) : base(previousEvent)
    {

    }

    public AutoEvent(Delegator delegator, DataCenter dataCenter, BoolCopier boolCopier) : base( delegator, dataCenter, boolCopier)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;
        this.boolCopier = boolCopier;
    }

    protected bool CopyConditionBool(bool conditionBool) => this.boolCopier.CopyConditionBool(conditionBool);

}
