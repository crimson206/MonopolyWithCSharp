public class DecisionFactorCalculator
{
    private IDataCenter dataCenter;

    public DecisionFactorCalculator(IDataCenter dataCenter)
    {
        this.dataCenter = dataCenter;
    }

    private List<IPropertyData> PropertyDatas => (from tileData in this.dataCenter.TileDatas where tileData is IPropertyData select (IPropertyData)tileData).ToList();
    private List<int> Balances => this.dataCenter.Bank.Balances;

    public int GetDangerFactor(int playerNumber)
    {
        int dangerFactor = this.Balances[playerNumber] / this.CalculateEnemiesTotalRent(playerNumber);
        return dangerFactor;
    }

    public int GetDangerFactor2(int playerNumber, int cost)
    {
        int dangerFactor = (this.Balances[playerNumber] - cost) / this.CalculateEnemiesTotalRent(playerNumber);
        throw new NotImplementedException();
    }

    public int GetAdventureFactor()
    {
        int freePropertyCount = this.CalculateFreePropertyCount();
        int totalPropertyCount = this.PropertyDatas.Count();
        return freePropertyCount / totalPropertyCount;
    }

    public int GetAgainstWinningPlayerFactor(int myPlayerNumber, int enemyPlayerNumber)
    {
        int myTotalRent = this.CalculateMyTotalRent(myPlayerNumber);
        int enemyTotalRent = this.CalculateMyTotalRent(enemyPlayerNumber);
        return myTotalRent / enemyTotalRent;
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

    private int CalculateEnemiesTotalRent(int playerNumber)
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
