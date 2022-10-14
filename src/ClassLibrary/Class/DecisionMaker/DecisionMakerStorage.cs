
public class DecisionMakerStorage
{

    public WantToUseJailFreeCard wantToUseJailFreeCard;
    public WantToPayJailFine wantToPayJailFine;

    public DecisionMakerStorage(Delegator delegator)
    {
        this.wantToUseJailFreeCard = new WantToUseJailFreeCard(delegator);
        this.wantToPayJailFine = new WantToPayJailFine(delegator);
    }
}
