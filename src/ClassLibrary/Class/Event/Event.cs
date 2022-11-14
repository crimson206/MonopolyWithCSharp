public abstract class Event
{
    protected Action lastEvent;
    protected IEvents? events;
    protected Delegator delegator;
    protected IDataCenter dataCenter;
    protected ITileManager tileManager;
    protected IPropertyManager propertyManager;

    public Event(Delegator delegator, IDataCenter dataCenter, ITileManager tileManager)
    {
        this.delegator = delegator;
        this.dataCenter = dataCenter;
        this.tileManager = tileManager;
        this.propertyManager = tileManager.PropertyManager;

        this.lastEvent = this.StartEvent;
    }

    protected int CurrentPlayerNumber => this.dataCenter.EventFlow.CurrentPlayerNumber;

    public void SetEvents(IEvents events)
    {
        this.events = events;
    }
    protected abstract void CallNextEvent();
    public abstract void StartEvent();

    public void AddNextAction(Action nextAction)
    {
        this.lastEvent = nextAction;

        this.delegator
            .SetNextAction(nextAction);
    }

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