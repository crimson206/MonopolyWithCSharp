public class DecisionMaker
{
    protected IDataCenter dataCenter;
    protected DecisionFactorCalculator decisionFactorCalculator;
    protected Random random = new Random();

    public DecisionMaker(IDataCenter dataCenter)
    {
        this.dataCenter = dataCenter;
        this.decisionFactorCalculator = new DecisionFactorCalculator(dataCenter);
    }

    protected bool ConvertProbabilityToResult(double probability)
    {
        int percent = (int)(probability * 100);
        int randomNumber = this.random.Next(0,101);

        if (percent >= randomNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
