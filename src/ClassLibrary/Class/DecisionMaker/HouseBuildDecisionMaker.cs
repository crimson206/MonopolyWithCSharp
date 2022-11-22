public class HouseBuildDecisionMaker : DecisionMaker, IHouseBuildDecisionMaker
{
    private Random random = new Random();

    public HouseBuildDecisionMaker(IDataCenter dataCenter)
        :base(dataCenter)
    {
        this.dataCenter = dataCenter;
    }

    private int? CurrentBuilder => this.dataCenter.HouseBuildHandler.CurrentHouseBuilder;
    private List<IRealEstateData> houseBuildableRealEstates => this.dataCenter.HouseBuildHandler.HouseBuildableRealEstatesOfOwners[(int)this.CurrentBuilder!];
    public int? ChooseRealEstateToBuildHouse()
    {
        int optionCount = this.houseBuildableRealEstates.Count();
        int randomNumber = random.Next(0, optionCount + 1);

        int? randomDecision = (randomNumber == optionCount? null : randomNumber);

        return randomDecision;
    }
}