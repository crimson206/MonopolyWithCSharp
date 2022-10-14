public abstract class DecisionMaker
{

    protected Delegator delegator;
    protected int playerNumber => delegator.CurrentPlayerNumber;
    protected enum Setting
    {
        Manual,
    }
    protected List<Enum> playerSettings = new List<Enum> {Setting.Manual, Setting.Manual, Setting.Manual, Setting.Manual };

    public DecisionMaker(Delegator delegator)
    {
        this.delegator = delegator;
    }

    public void MakeDecision()
    {

        switch (playerSettings[playerNumber])
        {
            case Setting.Manual:
                this.MakeDecisionManually();
                break;
            default:
                break;
        }
    }

    protected abstract void MakeDecisionManually();

}
