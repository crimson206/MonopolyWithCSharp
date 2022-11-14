public class HouseBuildHandler : IHouseBuildHandlerData
{
    private List<int> participantPlayerNumbers = new List<int>();
    private TileFilter tileFilter = new TileFilter();
    private List<IRealEstateData>? backupRealEstateDatas;
    private List<int>? backupBalances;
    private Dictionary<int, List<IRealEstateData>> houseBuildableRealEstatesOfOwners = new Dictionary<int, List<IRealEstateData>>();
    private bool areAnyBuildable;
    private int currentHouseBuilder;
    private int builderChangedCount;

    public bool AreAnyBuildable => this.areAnyBuildable;
    public List<int>? ParticipantPlayerNumbers => this.participantPlayerNumbers;
    public bool IsLastBuilder => (this.builderChangedCount == this.participantPlayerNumbers.Count() - 1? true : false);
    public Dictionary<int, List<IRealEstateData>> HouseBuildableRealEstatesOfOwners => this.HouseBuildableRealEstatesOfOwners;

    public void SetHouseBuildHandler(List<int> balances, List<IRealEstateData> realEstateDatas)
    {
        this.builderChangedCount = 0;
        this.backupRealEstateDatas = realEstateDatas;
        this.backupBalances = balances;
        this.SetAreAnyBuildable();
        if (this.areAnyBuildable)
        {
            this.SetHouseBuildableRealEstatesOfOwners();
            this.SetParticipantPlayerNumbers();
            this.currentHouseBuilder = this.participantPlayerNumbers![0];
        }
    }

    public void ChangeHouseBuilder()
    {
        if (this.participantPlayerNumbers is null)
        {
            throw new Exception();
        }

        this.builderChangedCount++;
        this.currentHouseBuilder = this.participantPlayerNumbers![this.builderChangedCount];
    }

    private void SetHouseBuildableRealEstatesOfOwners()
    {
        List<IRealEstateData> buildableRealEstateDatas = this.CreateReallyBuildableRealEstateDatas();

        foreach (var realEstateData in buildableRealEstateDatas)
        {
            int ownerNumber = (int)realEstateData.OwnerPlayerNumber!;

            if (this.houseBuildableRealEstatesOfOwners.Keys.Contains(ownerNumber) is false)
            {
                this.houseBuildableRealEstatesOfOwners.Add(ownerNumber, new List<IRealEstateData>());
            }

            this.houseBuildableRealEstatesOfOwners[ownerNumber].Add(realEstateData);
        }
    }

    private void SetAreAnyBuildable()
    {
        List<IRealEstateData> buildableRealEstateDatas = this.backupRealEstateDatas!
                                                        .Where(realEstate => realEstate.IsHouseBuildable)
                                                        .ToList();
        
        foreach (var realEstateData in buildableRealEstateDatas)
        {
            int ownerNumber = (int)realEstateData.OwnerPlayerNumber!;

            if (this.backupBalances![ownerNumber] >= realEstateData.BuildingCost)
            {
                this.areAnyBuildable = true;
            }

            this.areAnyBuildable = false;
        }
    }

    private List<IRealEstateData> CreateReallyBuildableRealEstateDatas()
    {
        List<IRealEstateData> filteredRealEstateDatas = new List<IRealEstateData>();

        foreach (var realEstateData in this.backupRealEstateDatas!)
        {
            if (realEstateData.IsHouseBuildable)
            {
                int ownerNumber = (int)realEstateData.OwnerPlayerNumber!;

                if (this.backupBalances![ownerNumber] >= realEstateData.BuildingCost)
                {
                    filteredRealEstateDatas.Add(realEstateData);
                }
            }
        }
        
        return filteredRealEstateDatas;
    }

    private void SetParticipantPlayerNumbers()
    {
        this.participantPlayerNumbers = this.houseBuildableRealEstatesOfOwners.Keys.ToList();
    }
}
