public class HouseBuildDecisionMaker : DecisionMaker, IHouseBuildDecisionMaker
{

    public HouseBuildDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
        this.dataCenter = dataCenter;
    }

    private int CurrentBuilder => (int)this.dataCenter.HouseBuildHandler.CurrentHouseBuilder!;
    private int CurrentBalance => this.dataCenter.Bank.Balances[CurrentBuilder];
    private List<IRealEstateData> houseBuildableRealEstates => this.dataCenter.HouseBuildHandler.HouseBuildableRealEstatesOfOwners[this.CurrentBuilder];
    public int? ChooseRealEstateToBuildHouse()
    {
        List<IRealEstateData> affordables = new List<IRealEstateData>();
        affordables = (from realEstate in this.houseBuildableRealEstates
                                        where realEstate.BuildingCost < this.CurrentBalance / 3
                                        select realEstate).ToList();

        if (affordables.Count() == 0)
        {
            return null;
        }

        int maxBuildingCost = (from realEstate in affordables
                                select realEstate.BuildingCost).Max();

        IRealEstateData target = (from realEstate in affordables
                                where realEstate.BuildingCost == maxBuildingCost
                                select realEstate).ToList()[0];

        int targetIndex = this.houseBuildableRealEstates.IndexOf(target);

        return targetIndex;
    }

    public List<IRealEstateData> FilterOutExpensiveOnes(List<IRealEstateData> realEstateDatas)
    {
        List<IRealEstateData> newList = realEstateDatas.Where(realEstate => realEstate.Price < this.CurrentBalance / 2).ToList();
   
        return newList;
    }

    public int GetIndexOfRealEstateWihtHighestRentIncrease(List<IRealEstateData> realEstateDatas)
    {
        List<int> increasedAmounts = new List<int>();

        foreach (var realEstateData in realEstateDatas)
        {
            int currentHouseCount = realEstateData.HouseCount;
            int currentRent = realEstateData.Rents[currentHouseCount + 1];        
            int newRent = realEstateData.Rents[currentHouseCount + 2];

            int increasedAmount = newRent - currentRent;

            increasedAmounts.Add(increasedAmount);
        }

        int output = increasedAmounts.IndexOf(increasedAmounts.Max());

        return output;
    }
}