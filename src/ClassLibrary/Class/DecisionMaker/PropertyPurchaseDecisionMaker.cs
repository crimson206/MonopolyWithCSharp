public class PropertyPurchaseDecisionMaker : DecisionMaker, IPropertyPurchaseDecisionMaker
{
    private Random random = new Random();

    public PropertyPurchaseDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }

    public bool MakeDecisionOnPurchase(int playerNumber)
    {
        int willingness = random.Next(0,10);
        
        if (willingness > 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}