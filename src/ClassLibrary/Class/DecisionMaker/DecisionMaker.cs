public class DecisionMaker
{
    protected IDataCenter dataCenter;
    protected DecisionFactorCalculator decisionFactorCalculator;

    public DecisionMaker(IDataCenter dataCenter)
    {
        this.dataCenter = dataCenter;
        this.decisionFactorCalculator = new DecisionFactorCalculator(dataCenter);
    }
}