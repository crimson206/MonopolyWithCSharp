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
    int BalanceOfTradeOwner => this.dataCenter.Bank.Balances[this.TradeOwner];

    public int? SelectTradeTarget()
    {
        IPropertyData? suitablePropertyToExchange = this.GetPropertyWhoseOnwerHasMoreThanOnePropertyToExchangeForMonopolyWithBestValue(this.TradeOwner, this.TradableProperties);
        

        if (suitablePropertyToExchange is null)
        {
            IPropertyData bestPropertyToGet = this.GetPropertyWithTheBestValueAmongAllTargetsProperties();
            double value = this.ConsiderMonopolyPriceBalanceAndEnemiesRentsWhenGettingAProperty(this.TradeOwner, bestPropertyToGet);
            
            if (value > 1)
            {
                return this.SelectableTargetNumbers.IndexOf((int)bestPropertyToGet.OwnerPlayerNumber!);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return this.SelectableTargetNumbers.IndexOf((int)suitablePropertyToExchange.OwnerPlayerNumber!);
        }
    }

    public int? SelectPropertyToGet()
    {
        IPropertyData propertyToGet;
        IPropertyData? suitablePropertyToExchange = this.GetPropertyWhoseOnwerHasMoreThanOnePropertyToExchangeForMonopolyWithBestValue(this.TradeOwner, this.TradableProperties)!;
        
        if (suitablePropertyToExchange is not null)
        {
            propertyToGet = suitablePropertyToExchange;
        }
        else
        {
            IPropertyData bestPropertyToGet = this.GetPropertyWithTheBestValueAmongAllTargetsProperties();
            propertyToGet = bestPropertyToGet;
        }

        int index = this.TradablePropertiesOfTarget.IndexOf(propertyToGet);

        return index;
    }

    public int? SelectPropertyToGive()
    {

        IPropertyData? suitablePropertyToExchange = this.GetPropertyWhoseOnwerHasMoreThanOnePropertyToExchangeForMonopolyWithBestValue(this.TradeOwner, this.TradableProperties)!;
        if (suitablePropertyToExchange is null)
        {
            return null;
        }

        List<IPropertyData> properties = this.FilterTradablePropertiesWhoseMembersOwnerIncluding(this.TradeOwner, this.TradeTarget, this.TradablePropertiesOfOwner);
        List<IPropertyData> propertiesAfterFilteringOutMembersOfPropertyToGet = new List<IPropertyData>();
        List<double> valuesOfProperties = new List<double>();
        IPropertyData worstPropertyToKeep;
        int index1OfWorstPropertyToKeep;
        int index2OfWorstPropertyToKeep;

        foreach (var property in properties)
        {
            if (property.Group.Contains(this.PropertyTradeOwnerToGet) is false)
            {
                propertiesAfterFilteringOutMembersOfPropertyToGet.Add(property);
            }
        }


        valuesOfProperties = this.propertyValueMeasurer.GetValuesAfterConsideringPriceAndMonopoly(this.TradeOwner, propertiesAfterFilteringOutMembersOfPropertyToGet);

        index1OfWorstPropertyToKeep = valuesOfProperties.IndexOf(valuesOfProperties.Min());

        worstPropertyToKeep =  propertiesAfterFilteringOutMembersOfPropertyToGet[index1OfWorstPropertyToKeep];

        index2OfWorstPropertyToKeep = this.TradablePropertiesOfOwner.IndexOf(worstPropertyToKeep);

        return index2OfWorstPropertyToKeep;

    }

    public int DecideAdditionalMoney()
    {

        double value = this.ConsiderPriceAndMonopolyWhenGettingAProperty(this.TradeOwner, this.PropertyTradeOwnerToGet!);
        int virtualPrice = (int)(value * this.PropertyTradeOwnerToGet!.Price);
        int priceOfPropertyToGive = 0;
        if(this.PropertyTradeOwnerToGive is not null)
        {
            priceOfPropertyToGive = this.PropertyTradeOwnerToGive.Price;
        }

        int priceGap = virtualPrice - priceOfPropertyToGive;
        int decision = 0;

        if(priceGap >= 0)
        {
            decision = (int)(priceGap * this.ConsiderBalanceCostAndEnemiesRents(this.TradeOwner, priceGap));
        }
        else
        {
            decision = priceGap;
        }
        return decision;
    }

    public bool MakeTradeTargetDecisionOnTradeAgreement()
    {
        double valueOfPropertyTradeTargetToGet = 0;
        int virtualPriceOfPropertyToGet = 0;
        double valueOfPropertyTradeTargetToGive = 0;
        int virtualPriceOfPropertyToGive = 0;
        if (this.PropertyTradeOwnerToGet is not null)
        {
            valueOfPropertyTradeTargetToGive = this.ConsiderPriceAndMonopoly(this.TradeTarget, this.PropertyTradeOwnerToGet!);
            virtualPriceOfPropertyToGive = (int)((double)this.PropertyTradeOwnerToGet!.Price * valueOfPropertyTradeTargetToGive);
        }

        if (this.PropertyTradeOwnerToGive is not null)
        {
            valueOfPropertyTradeTargetToGet = this.ConsiderPriceAndMonopolyWhenGettingAProperty(this.TradeTarget, this.PropertyTradeOwnerToGive!);
            virtualPriceOfPropertyToGet = (int)((double)this.PropertyTradeOwnerToGive!.Price * valueOfPropertyTradeTargetToGet);
        }

        if (virtualPriceOfPropertyToGet + this.AdditionalMoney > virtualPriceOfPropertyToGive)
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

    private List<IPropertyData> FilterTradablePropertiesWhoseMembersOwnerIncluding(int playerNumber1, int playerNumber2, List<IPropertyData> properties)
    {
        List<IPropertyData> filteredProperties = new List<IPropertyData>();

        foreach (var property in properties)
        {
            if (property.Group.Any(property => property.OwnerPlayerNumber == playerNumber1 && property.IsTradable)
            && property.Group.Any(property => property.OwnerPlayerNumber == playerNumber2 && property.IsTradable))
            {
                filteredProperties.Add(property);
            }
        }

        return filteredProperties;
    }

    private IPropertyData ExtractPropertyWithBestValueToGetConsideringPriceAndMonopoly(int playerNumber, List<IPropertyData> properties)
    {
        List<double> values = this.propertyValueMeasurer.GetValuesAfterConsideringPriceAndMonopolyWhenGettingAProperty(playerNumber, properties);
        int index = values.IndexOf(values.Max());

        IPropertyData property = properties[index];

        return property;
    }

    private IPropertyData? GetPropertyWhoseOnwerHasMoreThanOnePropertyToExchangeForMonopolyWithBestValue(int playerNumber, Dictionary<int, List<IPropertyData>> ownedTradableProperties)
    {
        IPropertyData? bestProperty = null;
        List<IPropertyData> bestProperties = new List<IPropertyData>();
        List<double> bestValues = new List<double>();
        int index = 0;

        foreach (var owner in ownedTradableProperties.Keys)
        {
            if (owner != playerNumber)
            {
                List<IPropertyData> properties = ownedTradableProperties[owner];
                List<IPropertyData> propertiesSuitableToExchange = this.FilterTradablePropertiesWhoseMembersOwnerIncluding(playerNumber, owner, properties);
                List<IPropertyData> propertiesFilteredOutCheaperOnesInTheSameGroup = this.FilterOutCheaperOnesIfMemberSizeIsLargerThanOne(propertiesSuitableToExchange);

                if (propertiesFilteredOutCheaperOnesInTheSameGroup.Count() >= 2)
                {
                    IPropertyData propertyWithBestValue = this.ExtractPropertyWithBestValueToGetConsideringPriceAndMonopoly(playerNumber, propertiesFilteredOutCheaperOnesInTheSameGroup);
                    bestProperties.Add(propertyWithBestValue);
                }
            }
        }
    
        bestValues = this.propertyValueMeasurer.GetValuesAfterConsideringPriceAndMonopolyWhenGettingAProperty(playerNumber, bestProperties);
        
        if (bestValues.Count() != 0)
        {
            index = bestValues.IndexOf(bestValues.Max());

            bestProperty = bestProperties[index];
        }

        return bestProperty;
    }

    private List<IPropertyData> FilterOutCheaperOnesIfMemberSizeIsLargerThanOne(List<IPropertyData> properties)
    {
        List<IPropertyData> copy = new List<IPropertyData>(properties);
        List<IPropertyData> propertiesToRemove = new List<IPropertyData>();

        int count = properties.Count();

        foreach (var property1 in properties)
        {
            foreach (var property2 in properties)
            {
                if (property1 != property2 && property1.Group == property2.Group)
                {
                    IPropertyData propertyToRemove = (property1.Price < property2.Price? property1 : property2);

                    if (copy.Contains(propertyToRemove))
                    {
                        copy.Remove(propertyToRemove);
                    }
                }

            }
        }

        return copy;
    }

    private IPropertyData GetPropertyWithTheBestValueAmongAllTargetsProperties()
    {
        List<IPropertyData> bestProperties = new List<IPropertyData>();

        foreach (var selectableTradeTarget in this.SelectableTargetNumbers)
        {
            IPropertyData bestPropertyOfOnePlayer = this.ExtractPropertyWithBestValueToGetConsideringPriceAndMonopoly(this.TradeOwner, this.TradableProperties[selectableTradeTarget]);
            bestProperties.Add(bestPropertyOfOnePlayer);
        }

        IPropertyData bestProperty = this.ExtractPropertyWithBestValueToGetConsideringPriceAndMonopoly(this.TradeOwner, bestProperties);
        return bestProperty;
    }
}
