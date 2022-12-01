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
        double denominator = balance + cost + this.decisionFactorCalculator.CalculateEnemiesTotalRent(playerNumber);

        return balance / denominator;
    }

    protected double ConsiderMonopolyPriceBalanceAndEnemiesRentsWhenGettingAProperty(int player, IPropertyData property)
    {
        int price = property.Price;
        double factor1 = this.propertyValueMeasurer.ConsiderPriceAndMonopolyWhenGettingAProperty(player, property);
        double factor2 = this.ConsiderBalanceCostAndEnemiesRents(player, (int)(factor1 * price));

        double value = factor1 * factor2;

        return value;
    }
}
