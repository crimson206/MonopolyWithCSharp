public class HouseBuildHandler : IHouseBuildHandlerData
{
    private List<int> participantPlayerNumbers = new List<int>();
    private TileFilter tileFilter = new TileFilter();
    private List<IRealEstateData>? backupRealEstateDatas;
    private List<int>? backupBalances;
    private Dictionary<int, List<IRealEstateData>> houseBuildableRealEstatesOfOwners = new Dictionary<int, List<IRealEstateData>>();
    private bool? areAnyBuildable;
    private int? currentHouseBuilder;
    private int? builderChangedCount;
    private IRealEstateData? realEstateToBuildHouse;

    public bool? AreAnyBuildable => this.areAnyBuildable;
    public List<int>? ParticipantPlayerNumbers => this.participantPlayerNumbers;
    public bool IsLastBuilder => (this.builderChangedCount == this.participantPlayerNumbers.Count() - 1? true : false);
    public Dictionary<int, List<IRealEstateData>> HouseBuildableRealEstatesOfOwners => this.houseBuildableRealEstatesOfOwners;
    public IRealEstateData? RealEstateToBuildHouse => this.realEstateToBuildHouse;
    public int? CurrentHouseBuilder => this.currentHouseBuilder;
    public List<IRealEstateData> HouseBuildableRealEstatesOfCurrentBuilder => this.houseBuildableRealEstatesOfOwners[(int)this.currentHouseBuilder!];

    public void SetHouseBuildHandler(List<int> balances, List<IRealEstateData> realEstateDatas)
    {
        this.builderChangedCount = 0;
        this.realEstateToBuildHouse = null;
        this.backupRealEstateDatas = realEstateDatas;
        this.backupBalances = balances;
        this.SetAreAnyBuildable();
        if (this.areAnyBuildable is true)
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

        this.realEstateToBuildHouse = null;
        this.builderChangedCount++;
        this.currentHouseBuilder = this.participantPlayerNumbers![(int)this.builderChangedCount!];
    }

    public void SetRealEstateToBuildHouse(IRealEstateData realEstateToBuildHouse)
    {
        if (this.houseBuildableRealEstatesOfOwners[(int)this.currentHouseBuilder!].Contains(realEstateToBuildHouse) is false)
        {
            throw new Exception();
        }

        this.realEstateToBuildHouse = realEstateToBuildHouse;
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
                return;
            }
        }

        this.areAnyBuildable = false;
        return;
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
        this.participantPlayerNumbers.Sort();
    }
}
