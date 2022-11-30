public class PropertyValueMeasurer
{
    private DecisionFactorCalculator decisionFactorCalculator;

    public PropertyValueMeasurer(IDataCenter dataCenter)
    {
        this.decisionFactorCalculator = new DecisionFactorCalculator(dataCenter); 
    }


    public double ConsiderPriceAndMonopolyWhenGettingAProperty(int player, IPropertyData property)
    {
        double factor1 = this.ConsiderMonopolyWhenGettingAProperty(player, property) *
                                        this.ConsiderPriceRank(property);
        double factor2 = this.ConsiderFreePropertyCountPerTotalCount(factor1);

        return factor1;
    }

    public double ConsiderPriceAndMonopoly(int player, IPropertyData property)
    {
        double factor1 = this.ConsiderMonopoly(player, property) *
                                        this.ConsiderPriceRank(property);
        double factor2 = this.ConsiderFreePropertyCountPerTotalCount(factor1);

        return factor2;
    }

    public List<double> GetValuesAfterConsideringPriceAndMonopoly(int player, List<IPropertyData> properties)
    {
        List<double> values = new List<double>();

        foreach (var property in properties)
        {
            values.Add(this.ConsiderPriceAndMonopoly(player, property));
        }

        return values;
    }

    public List<double> GetValuesAfterConsideringPriceAndMonopolyWhenGettingAProperty(int player, List<IPropertyData> properties)
    {
        if (properties.Any(property => property.OwnerPlayerNumber == player))
        {
            throw new Exception();
        }

        List<double> values = new List<double>();

        foreach (var property in properties)
        {
            values.Add(this.ConsiderPriceAndMonopolyWhenGettingAProperty(player, property));
        }

        return values;
    }
    public double ConsiderPriceRank(IPropertyData property)
    {
        double priceRank = this.decisionFactorCalculator.CalculatePropertysPriceRank(property);

        double output = 1 + ((priceRank - 0.4) / 3);

        return output;
    }

    public double ConsiderMonopolyWhenGettingAProperty(int player, IPropertyData property)
    {

        int groupSize = property.Group.Count();
        int howCloseToMonopolyAfterGetting = this.decisionFactorCalculator.CalculateCanMonopoly(player, property) - 1;
        double output;

        if (property is IRealEstateData)
        {
            output = this.ConsiderRealEstateMonopoly(groupSize, howCloseToMonopolyAfterGetting);
        }
        else
        {
            output = this.ConsiderNonRealEstateMonopoly(groupSize, howCloseToMonopolyAfterGetting);
        }

        return output;
    }

    public double ConsiderMonopoly(int player, IPropertyData property)
    {

        int groupSize = property.Group.Count();
        int howCloseToMonopoly = this.decisionFactorCalculator.CalculateCanMonopoly(player, property);
        double output = 0;

        if (property is IRealEstateData)
        {
            output = this.ConsiderRealEstateMonopoly(groupSize, howCloseToMonopoly);
        }
        else
        {
            output = this.ConsiderNonRealEstateMonopoly(groupSize, howCloseToMonopoly);
        }

        return output;
    }

    public double ConsiderFreePropertyCountPerTotalCount(double factor)
    {
        double considered = factor;
        
        if (factor > 1)
        {
        considered = 1 + (factor - 1) * this.decisionFactorCalculator.CalculateFreePropertyCountPerTotalCount();
        }

        return considered;
    }

    public double ConsiderRealEstateMonopoly(int groupSize, int howCloseToMonopoly)
    {
        double preferPropertyCloserToMonopoly = (groupSize + 1) / (double)(howCloseToMonopoly + 1);

        return preferPropertyCloserToMonopoly;
    }

    public double ConsiderNonRealEstateMonopoly(int groupSize, int howCloseToMonopoly)
    {
        double preferPropertyCloserToMonopoly = (groupSize + 2) / (double)(howCloseToMonopoly + 2);

        return preferPropertyCloserToMonopoly;
    }
}
