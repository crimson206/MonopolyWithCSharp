public class WantToPayJailFine : DecisionMaker
{

    private new enum Setting
    {
        Manual,
    }

    public WantToPayJailFine(PromptDrawer prompter, Delegator delegator) : base(prompter, delegator)
    {

    }

    public override void MakeDecision()
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

    protected override void MakeDecisionManually()
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
