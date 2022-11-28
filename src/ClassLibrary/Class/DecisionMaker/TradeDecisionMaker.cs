public class TradeDecisionMaker : PropertyDecisionMaker, ITradeDecisionMaker
{

    public TradeDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
    }

    int TradeOwner => (int)this.dataCenter.TradeHandler.CurrentTradeOwner!;
    int TradeTarget => (int)this.dataCenter.TradeHandler.CurrentTradeTarget!;
    List<int> SelectableTargetNumbers => this.dataCenter.TradeHandler.SelectableTargetNumbers;
    Dictionary<int, List<IPropertyData>> TradableProperties => this.dataCenter.TradeHandler.OwnedTradablePropertyDatas!;
    List<IPropertyData> TradablePropertiesOfTarget => this.dataCenter.TradeHandler.TradablePropertiesOfTradeTarget;
    List<IPropertyData> TradablePropertiesOfOwner => this.dataCenter.TradeHandler.TradablePropertiesOfTradeOwner;
    IPropertyData? PropertyTradeOwnerToGet => this.dataCenter.TradeHandler.PropertyTradeOwnerToGet;
    IPropertyData? PropertyTradeOwnerToGive => this.dataCenter.TradeHandler.PropertyTradeOwnerToGive;
    int AdditionalMoney => this.dataCenter.TradeHandler.MoneyOwnerWillingToAddOnTrade;
    int BalanceOfTradeTarget => this.dataCenter.Bank.Balances[this.TradeTarget];

    public int? SelectTradeTarget()
    {
        List<int> players = this.SelectableTargetNumbers;
        List<double> maxValues = new List<double>();
        List<int> indecesOfBestProperties = new List<int>();

        foreach (var player in players)
        {
            if (this.TradableProperties[player].Count() != 0)
            {
                List<double> realPrices = (from property in this.TradableProperties[player]
                                        select (double)property.Price).ToList();
                List<double> values = this.propertyValueMeasurer
                                        .GetValuesAfterConsideringPriceAndMonopolyWhenGettingAProperty(this.TradeOwner, this.TradableProperties[player]);

                indecesOfBestProperties.Add(values.IndexOf(values.Max()));
                maxValues.Add(values.Max());
            }
        }

        if (maxValues.Max() > 1.5)
        {
            int indexOfTargetPlayer = maxValues.IndexOf(maxValues.Max());

            return indexOfTargetPlayer;
        }
        else
        {
            return null;
        }
    }

    public int? SelectPropertyToGet()
    {
        List<double> values = this.propertyValueMeasurer
                                        .GetValuesAfterConsideringPriceAndMonopolyWhenGettingAProperty(this.TradeOwner, this.TradableProperties[this.TradeTarget]);
        int index = values.IndexOf(values.Max());

        return index;
    }

    public int? SelectPropertyToGive()
    {
        if (this.TradablePropertiesOfOwner.Count() == 0)
        {
            return null;
        }

        double valueOfPropertyToGet = this.propertyValueMeasurer
                                    .ConsiderPriceAndMonopolyWhenGettingAProperty(this.TradeOwner, this.PropertyTradeOwnerToGet!);

        List<double> valuesOfPropertiesOfTradeOwner = new List<double>();
        List<IPropertyData> propertiesInMind = new List<IPropertyData>();
        List<double> valuesOfPropertiesInMind = new List<double>();

        foreach (var property in TradablePropertiesOfOwner)
        {
            if (property.Group.Contains(this.PropertyTradeOwnerToGet) is false
            && property.OwnerPlayerNumber == this.TradeTarget)
            {
                propertiesInMind.Add(property);
                valuesOfPropertiesInMind.Add(this.propertyValueMeasurer.ConsiderPriceAndMonopoly(this.TradeOwner, property));
            }
        }

        if(propertiesInMind.Count() == 0)
        {
            return null;
        }

        if(valuesOfPropertiesInMind.Min() > valueOfPropertyToGet)
        {
            return null;
        }


        int index1 = valuesOfPropertiesInMind.IndexOf(valuesOfPropertiesInMind.Min());
        IPropertyData PropertyToGive = propertiesInMind[index1];
        int index2 = this.TradablePropertiesOfOwner.IndexOf(PropertyToGive);

        return index2;

    }

    public int DecideAdditionalMoney()
    {
        int virtualMoneyToAdd = 0;
        int virtualMoneyToSubstract = 0;
        double factor1;
        double factor2;
        double factor3;
        int output = 0;

        if (this.PropertyTradeOwnerToGet is not null)
        {
            factor1 = this.propertyValueMeasurer.ConsiderPriceAndMonopolyWhenGettingAProperty(this.TradeOwner, this.PropertyTradeOwnerToGet);
            virtualMoneyToAdd = (int)(this.PropertyTradeOwnerToGet.Price * factor1);
        }
        
        if (this.PropertyTradeOwnerToGive is not null)
        {
            factor2 = this.propertyValueMeasurer.ConsiderPriceAndMonopoly(this.TradeOwner, this.PropertyTradeOwnerToGive);
            virtualMoneyToSubstract = (int)(this.PropertyTradeOwnerToGive.Price * factor2);
        }


        if (virtualMoneyToAdd - virtualMoneyToSubstract > 0)
        {
            factor3 = this.ConsiderBalanceCostAndEnemiesRents(this.TradeOwner, virtualMoneyToAdd - virtualMoneyToSubstract);
            output = (int)(factor3 * (virtualMoneyToAdd - virtualMoneyToSubstract));

            return output;
        }
        else if (virtualMoneyToSubstract - virtualMoneyToAdd > this.BalanceOfTradeTarget)
        {
            output = this.BalanceOfTradeTarget;

            return output;
        }
        else
        {
            return virtualMoneyToAdd - virtualMoneyToSubstract;
        }
    }

    public bool MakeTradeTargetDecisionOnTradeAgreement()
    {
        double revaluedPriceOfPropertyTradeTargetToGive = 0;
        double revaluedPriceOfPropertyTradeTargetToGet = 0;
        double factor1;
        double factor2;

        if (this.PropertyTradeOwnerToGet is not null)
        {
            factor1 = this.propertyValueMeasurer.ConsiderPriceAndMonopoly(this.TradeTarget, this.PropertyTradeOwnerToGet);
            revaluedPriceOfPropertyTradeTargetToGive = factor1 * this.PropertyTradeOwnerToGet.Price;
        }

        if (this.PropertyTradeOwnerToGive is not null)
        {
            factor2 = this.propertyValueMeasurer.ConsiderPriceAndMonopolyWhenGettingAProperty(this.TradeTarget, this.PropertyTradeOwnerToGive);
            revaluedPriceOfPropertyTradeTargetToGet = factor2 * this.PropertyTradeOwnerToGive.Price;
        }

        int calculatedPropertiesValueGap = (int)(revaluedPriceOfPropertyTradeTargetToGet - revaluedPriceOfPropertyTradeTargetToGive);

        if (calculatedPropertiesValueGap > 0)
        {
            double balanceCostAndEnemiesRentsConsidered = this.ConsiderBalanceCostAndEnemiesRents(this.TradeTarget, calculatedPropertiesValueGap);
            int moneyTradeTargetWillingToPay = (int)(calculatedPropertiesValueGap * balanceCostAndEnemiesRentsConsidered);

            if (moneyTradeTargetWillingToPay >= - this.AdditionalMoney)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        if (calculatedPropertiesValueGap + this.AdditionalMoney > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool HasPropertyMemberWhoseOwnerIs(IPropertyData propertyData, int playerNumber)
    {
        bool ownerOfAnyIsThePlayer = propertyData.Group.Any(property => property.OwnerPlayerNumber == playerNumber);
        
        return ownerOfAnyIsThePlayer;
    }
}
