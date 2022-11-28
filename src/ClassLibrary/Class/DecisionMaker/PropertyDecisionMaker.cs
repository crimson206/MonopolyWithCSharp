public abstract class PropertyDecisionMaker : DecisionMaker
{
    protected PropertyValueMeasurer propertyValueMeasurer;
    public PropertyDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
        this.propertyValueMeasurer = new PropertyValueMeasurer(dataCenter);
    }

    protected double ConsiderBalanceCostAndEnemiesRents(int playerNumber, int cost)
    {

        if (cost < 0)
        {
            throw new Exception();
        }

        double balance = (double)this.dataCenter.Bank.Balances[playerNumber];
        double denominator = 0.5 * balance + cost + this.decisionFactorCalculator.CalculateEnemiesTotalRent(playerNumber);

        return balance / denominator;
    }
}
