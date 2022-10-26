public class Analyser
{
    private List<Property> properties;
    private List<RealEstate> realEstates;

    public Analyser(List<Property> properties, List<RealEstate> realEstates)
    {
        this.properties = properties;
        this.realEstates = realEstates;
    }

    public List<int> TotalPricesOfProerties => this.GetTotalPriceList();
    public List<int> TotalRentsOfProerties => this.GetTotalRentList();

    public bool IsAbleToMonopoly(int playerNumber, RealEstate realEstate)
    {
        List<RealEstate> colorGroup = GetColorGroup(realEstate);
        int numFreeRealEstates = colorGroup.Where(member => member.OwnerPlayerNumber == null).ToList().Count();
        int numMyRealEstates = colorGroup.Where(member => member.OwnerPlayerNumber == playerNumber).ToList().Count();
        int numTotalGroup = colorGroup.Count();
        if ( numFreeRealEstates + numMyRealEstates == numTotalGroup)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<double> ConvertRealEstateToBuildingHouseCoseEfficiency(List<RealEstate> realEstates)
    {
        var query = from realEstate in realEstates select this.CalBuildingHouseCostEfficiency(realEstate);
        List<double> costEfficiencies = query.ToList();
        return costEfficiencies;
    }

    private double CalBuildingHouseCostEfficiency(RealEstate realEstate)
    {
        if (realEstate.IsHouseBuildable is false)
        {
            return 0;
        }
        else
        {
            int currentRent = realEstate.CurrentRent;
            int currentNumHouses = realEstate.Rents.IndexOf(currentRent);
            int nextRent = realEstate.Rents[currentNumHouses+1];
            double costEfficiency = (double) (nextRent - currentRent)/realEstate.BuildingCost;
            return costEfficiency;
        }
    }

    private List<int> GetTotalPriceList()
    {
        List<int> totalPrices = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            List<int> pricesOfPropertiesUnderPlayeri = (from aaaa in this.properties where aaaa.OwnerPlayerNumber == i select aaaa.Price).ToList();
            int priceSum = pricesOfPropertiesUnderPlayeri.Sum();
            totalPrices.Add(priceSum);
        }
        return totalPrices;
    }

    private List<int> GetTotalRentList()
    {
        List<int> totalRents = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            List<Property> propertiesUnderSameOwner = this.properties.Where(property => property.OwnerPlayerNumber == i).ToList();
            int rentSum = this.CalTotalRentOfProperties(propertiesUnderSameOwner);
            totalRents.Add(rentSum);
        }
        return totalRents;
    }

    private int CalTotalPriceOfProperties(List<Property> properties)
    {
        var query = from property in properties select property.Price;
        int priceSum = query.ToList().Sum();
        return priceSum;
    }

    private int CalTotalRentOfProperties(List<Property> properties)
    {
        var query = from property in properties select property.CurrentRent;
        int rentSum = query.ToList().Sum();
        return rentSum;
    }

    private List<RealEstate> GetColorGroup(RealEstate realEstate)
    {
        List<RealEstate> colorGroup = this.realEstates.Where(member => member.Color == realEstate.Color).ToList();
        return colorGroup;
    }

    public void Test()
    {
        List<int> a = (from aaaa in this.properties where aaaa.OwnerPlayerNumber == 0 select aaaa.Price).ToList();
        List<int> c = (from aaaa in this.realEstates where aaaa.OwnerPlayerNumber == 0 select aaaa.Price).ToList();
        List<int> b = (from aaaa in this.properties where aaaa.OwnerPlayerNumber == 0 select aaaa.Price).ToList();

    }

}