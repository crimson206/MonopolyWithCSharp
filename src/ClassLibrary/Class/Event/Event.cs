public abstract class Event : IEvent
{
    protected Action lastAction;
    protected IEvents? events;
    protected Delegator delegator;
    protected IDataCenter dataCenter;
    protected ITileManager tileManager;
    protected IPropertyManager propertyManager;
    protected EventFlow eventFlow;

    public Event(Delegator delegator, IDataCenter dataCenter, ITileManager tileManager, IStatusHandlers statusHandlers)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;
        this.tileManager = tileManager;
        this.propertyManager = tileManager.PropertyManager;
        this.eventFlow = statusHandlers.EventFlow;

        this.lastAction = this.StartEvent;
    }

    public IEvent? LastEvent { get; set; }

    public void SetEvents(IEvents events)
    {
        this.events = events;
    }
    protected abstract void CallNextAction();
    public abstract void StartEvent();

    public virtual void AddNextAction(Action nextAction)
    {
        this.lastAction = nextAction;

        this.delegator
            .SetNextAction(nextAction);
    }

    public abstract void EndEvent();

    protected string ConvertIntListToString(List<int> ints)
    {
        int intCount = ints.Count();
        string convertedString = string.Empty;
        for (int i = 0; i < intCount; i++)
        {
            convertedString += ints[i];

            if(i != intCount - 1)
            {
                convertedString += ", ";
            }
        }

        return convertedString;
    }
}