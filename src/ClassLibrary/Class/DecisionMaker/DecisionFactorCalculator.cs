public class DecisionFactorCalculator
{
    private IDataCenter dataCenter;
    private int minPriceOfProperties;
    private int maxPriceOfProperties;

    public DecisionFactorCalculator(IDataCenter dataCenter)
    {
        this.dataCenter = dataCenter;
        List<IPropertyData> properties = (from tile in this.dataCenter.TileDatas
                            where tile is IPropertyData
                            select tile as IPropertyData).ToList();
        List<int> priceList = (from property in properties
                            where property is IPropertyData
                            select property.Price).ToList();
        this.minPriceOfProperties = priceList.Min();
        this.maxPriceOfProperties = priceList.Max();
    }

    private List<IPropertyData> PropertyDatas => (from tileData in this.dataCenter.TileDatas where tileData is IPropertyData select (IPropertyData)tileData).ToList();
    private List<int> Balances => this.dataCenter.Bank.Balances;

    public double CalculateRestBalancePerEnemiesTotalRents(int playerNumber, int cost)
    {
        double dangerFactor = (double)(this.Balances[playerNumber] - cost) /
                            ((double)this.CalculateEnemiesTotalRent(playerNumber) + 1);

        return dangerFactor;
    }

    public double CalculateCostPerBalance(int playerNumber, int cost)
    {
        double costDevidedByBalance = (double)cost /
                                    (double)this.Balances[playerNumber];

        return costDevidedByBalance;
    }

    public double CalculateFreePropertyCountPerTotalCount()
    {
        int freePropertyCount = this.CalculateFreePropertyCount();
        int totalPropertyCount = this.PropertyDatas.Count();

        return (double)freePropertyCount / (double)totalPropertyCount;
    }

    public double CalculateMyTotalRentPerEnemyTotalRent(int myPlayerNumber, int enemyPlayerNumber)
    {
        int myTotalRent = this.CalculateMyTotalRent(myPlayerNumber);
        int enemyTotalRent = this.CalculateMyTotalRent(enemyPlayerNumber);
        return (double)myTotalRent / (double)enemyTotalRent;
    }

    public int CalculateCanMonopoly(int playerNumber, IPropertyData property)
    {

        int sizeOfGroupOfProperty = property.Group.Count();
        int playersPropertyCount = property.Group.Where(member => member.OwnerPlayerNumber == playerNumber).Count();

        return sizeOfGroupOfProperty - playersPropertyCount;
    }

    public double CalculatePropertysPriceRank(IPropertyData propertyData)
    {

        double priceRank = (double)(propertyData.Price - this.minPriceOfProperties) 
                            / (double)(this.maxPriceOfProperties - this.minPriceOfProperties);

        return priceRank;
    }

    public double CalculateIncreasedRentPerBuildingCost(IRealEstateData realEstateData)
    {
        int houseCount = realEstateData.HouseCount;
        int increasedRent = realEstateData.Rents[houseCount + 2] / realEstateData.Rents[houseCount + 1];
        double increasedRentPerBuildingCost = (double)increasedRent / (double)realEstateData.BuildingCost;
    
        return increasedRentPerBuildingCost;
    }

    public double CalculateDecreasedRentPerBuildingCost(IRealEstateData realEstateData)
    {
        int houseCount = realEstateData.HouseCount;

        if (houseCount == 0)
        {
            throw new Exception();
        }

        int increasedRent = realEstateData.Rents[houseCount + 1] / realEstateData.Rents[houseCount];
        double decreasedRentPerBuildingCost = (double)increasedRent / (double)realEstateData.BuildingCost;
    
        return decreasedRentPerBuildingCost;  
    }

    public double CalculateAveragePrice()
    {
        double averagePrice = (
            from property in this.PropertyDatas
            select (double)property.Price
        ).Average();

        return averagePrice;
    }

    private int CalculateMyTotalRent(int playerNumber)
    {
        List<IPropertyData> myProperties = this.FilterWithPlayerNumber(playerNumber);
        int myTotalRent = 0;
        foreach (var property in myProperties)
        {
            if (property is IUtilityData)
            {
                myTotalRent += 6 * property.CurrentRent;
            }
            else
            {
                myTotalRent += property.CurrentRent;
            }
        }
        return myTotalRent;
    }

    public int CalculateEnemiesTotalRent(int playerNumber)
    {
        List<IPropertyData> myProperties = this.ReverseFilterWithPlayerNumber(playerNumber);
        int enemiesTotalRent = 0;
        foreach (var property in myProperties)
        {
            if (property is IUtilityData)
            {
                enemiesTotalRent += 6 * property.CurrentRent;
            }
            else
            {
                enemiesTotalRent += property.CurrentRent;
            }
        }
        return enemiesTotalRent;  
    }

    private List<IPropertyData> FilterWithPlayerNumber(int playerNumber)
    {
        List<IPropertyData> filteredPropertyDatas =
        this.PropertyDatas.Where(property => property.OwnerPlayerNumber == playerNumber).ToList();
        return filteredPropertyDatas;
    }

    private List<IPropertyData> ReverseFilterWithPlayerNumber(int exceptPlayerNumber)
    {
        List<IPropertyData> filteredPropertyDatas =
        this.PropertyDatas.Where(property => property.OwnerPlayerNumber != exceptPlayerNumber).ToList()
        .Where(property => property.OwnerPlayerNumber != null).ToList();
        return filteredPropertyDatas;
    }

    private int CalculateFreePropertyCount()
    {
        int freePropertyCount = this.PropertyDatas.Where(property => property.OwnerPlayerNumber is null).Count();
        return freePropertyCount;
    }

}
