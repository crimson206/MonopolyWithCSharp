public abstract class DecisionMaker
{
    protected PromptDrawer prompter;
    protected Delegator delegator;

    protected enum Setting
    {
        Manual,
    }
    protected List<Enum> playerSettings = new List<Enum> {Setting.Manual, Setting.Manual, Setting.Manual, Setting.Manual };

    public DecisionMaker(PromptDrawer prompter, Delegator delegator)
    {
        this.prompter = prompter;
        this.delegator = delegator;
    }

    public abstract void MakeDecision();

    protected abstract void MakeDecisionManually();

}
