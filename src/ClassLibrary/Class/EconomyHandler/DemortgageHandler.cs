public class DemortgageHandler : IDemortgageHandlerdata
{
    private List<int> participantPlayerNumbers = new List<int>();
    private TileFilter tileFilter = new TileFilter();
    private List<IPropertyData>? backupPropertyDatas;
    private List<int>? backupBalances;
    private Dictionary<int, List<IPropertyData>> deMortgagiblePropertiesOfOwners = new Dictionary<int, List<IPropertyData>>();
    private bool? areAnyDeMortgagible;
    private int? currentPlayerToDeMortgage;
    private int playerChangedCount = -1;
    private IPropertyData? propertyToDeMortgage;

    public bool? AreAnyBuildable => this.areAnyDeMortgagible;
    public List<int>? ParticipantPlayerNumbers => this.participantPlayerNumbers;
    public bool IsLastBuilder => (this.playerChangedCount == this.participantPlayerNumbers.Count() - 1? true : false);
    public Dictionary<int, List<IPropertyData>> DeMortgagiblePropertiesOfOwners => this.deMortgagiblePropertiesOfOwners;
    public IPropertyData? PropertyToDeMortgage => this.propertyToDeMortgage;
    public int? CurrentPlayerToDeMortgage => this.currentPlayerToDeMortgage;
    public List<IPropertyData> DeMortgagiblePropertiesOfCurrentPlayer => this.deMortgagiblePropertiesOfOwners[(int)this.currentPlayerToDeMortgage!];

    public void SetDeMortgageHandler(List<int> balances, List<IPropertyData> properties)
    {
        this.playerChangedCount = 0;
        this.propertyToDeMortgage = null;
        this.backupPropertyDatas = properties;
        this.backupBalances = balances;
        this.SetAreAnyDeMortgagible();
        if (this.areAnyDeMortgagible is true)
        {
            this.SetDeMortgagiblePropertiesOfOwners();
            this.SetParticipantPlayerNumbers();
            this.currentPlayerToDeMortgage = this.participantPlayerNumbers![0];
        }
    }

    public void ChangePlayerToDeMortgage()
    {
        if (this.participantPlayerNumbers is null)
        {
            throw new Exception();
        }

        this.propertyToDeMortgage = null;
        this.playerChangedCount++;
        this.currentPlayerToDeMortgage = this.participantPlayerNumbers![(int)this.playerChangedCount!];
    }

    public void SetRealEstateToBuildHouse(int indexOfRealEstate)
    {
        this.propertyToDeMortgage = DeMortgagiblePropertiesOfCurrentPlayer[indexOfRealEstate];
    }

    private void SetDeMortgagiblePropertiesOfOwners()
    {
        List<IPropertyData> buildableRealEstateDatas = this.CreateReallyBuildableRealEstateDatas();
        this.deMortgagiblePropertiesOfOwners.Clear();

        foreach (var propertyData in buildableRealEstateDatas)
        {
            int ownerNumber = (int)propertyData.OwnerPlayerNumber!;

            if (this.deMortgagiblePropertiesOfOwners.Keys.Contains(ownerNumber) is false)
            {
                this.deMortgagiblePropertiesOfOwners.Add(ownerNumber, new List<IPropertyData>());
            }

            this.deMortgagiblePropertiesOfOwners[ownerNumber].Add(propertyData);
        }
    }

    private void SetAreAnyDeMortgagible()
    {
        List<IPropertyData> buildableRealEstateDatas = this.backupPropertyDatas!
                                                        .Where(realEstate => realEstate.IsMortgaged)
                                                        .ToList();
        
        foreach (var realEstateData in buildableRealEstateDatas)
        {
            int ownerNumber = (int)realEstateData.OwnerPlayerNumber!;

            if (this.backupBalances![ownerNumber] >= 1.1 * realEstateData.Mortgage)
            {
                this.areAnyDeMortgagible = true;
                return;
            }
        }

        this.areAnyDeMortgagible = false;
        return;
    }

    private List<IPropertyData> CreateReallyBuildableRealEstateDatas()
    {
        List<IPropertyData> filteredPropertyDatas = new List<IPropertyData>();

        foreach (var propertyData in this.backupPropertyDatas!)
        {
            if (propertyData.IsMortgaged)
            {
                int ownerNumber = (int)propertyData.OwnerPlayerNumber!;

                if (this.backupBalances![ownerNumber] >= 1.1 * propertyData.Mortgage)
                {
                    filteredPropertyDatas.Add(propertyData);
                }
            }
        }
        
        return filteredPropertyDatas;
    }

    private void SetParticipantPlayerNumbers()
    {
        this.participantPlayerNumbers = this.deMortgagiblePropertiesOfOwners.Keys.ToList();
        this.participantPlayerNumbers.Sort();
    }
}
