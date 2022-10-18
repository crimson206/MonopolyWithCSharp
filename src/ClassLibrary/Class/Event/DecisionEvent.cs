public abstract class DecisionEvent
{
    protected enum Setting
    {
        Manual,
        Default
    }
    
    protected List<Enum> playerSettings = new List<Enum> {Setting.Default, Setting.Manual, Setting.Manual, Setting.Manual };
}
