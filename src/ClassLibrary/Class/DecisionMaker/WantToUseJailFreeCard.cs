public class WantToUseJailFreeCard : DecisionMaker
{
    private PromptDrawer prompter;
    private Delegator delegator;
    private enum Setting
    {
        Manual,
    }
    private List<Enum> playerSettings = new List<Enum> {Setting.Manual, Setting.Manual, Setting.Manual, Setting.Manual };

    public void MakeDecision()
    {
        int playerNumber = delegator.CurrentPlayerNumber;
        switch (playerSettings[playerNumber])
        {
            case Setting.Manual:
                this.MakeDecisionManually();
                break;
            default:
                delegator.BoolDecision = true;
                break;
        }

    }

    private void MakeDecisionManually()
    {
        prompter.PromptBool();
        if (prompter.ReceivedBool == null)
        {
            return;
        }
        else
        {
            delegator.BoolDecision = prompter.ReceivedBool;
            prompter.ResetPrompt();
            delegator.makeDecision = null;
        }
    }
}
