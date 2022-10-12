
public class DecisionMakerStorage
{

    public WantToUseJailFreeCard wantToUseJailFreeCard;
    public WantToPayJailFine wantToPayJailFine;

    public DecisionMakerStorage
    (
        PromptDrawer prompter, Delegator delegator
    )
    {
        this.wantToUseJailFreeCard = new WantToUseJailFreeCard(prompter, delegator);
        this.wantToPayJailFine = new WantToPayJailFine(prompter, delegator);
    }
}
