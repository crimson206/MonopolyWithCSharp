public class DemortgageHandler : IDemortgageHandlerData
{
    private List<int> participantPlayerNumbers = new List<int>();
    private TileFilter tileFilter = new TileFilter();
    private List<IPropertyData>? backupPropertyDatas;
    private List<int>? backupBalances;
    private Dictionary<int, List<IPropertyData>> demortgagiblePropertiesOfOwners = new Dictionary<int, List<IPropertyData>>();
    private bool? areAnyDemortgagible;
    private int? currentPlayerToDemortgage;
    private int playerChangedCount = 0;
    private IPropertyData? propertyToDemortgage;

    public bool? AreAnyDemortgagible => this.areAnyDemortgagible;
    public List<int>? ParticipantPlayerNumbers => this.participantPlayerNumbers;
    public bool IsLastPlayer => (this.playerChangedCount == this.participantPlayerNumbers.Count() - 1? true : false);
    public Dictionary<int, List<IPropertyData>> DeMortgagiblePropertiesOfOwners => this.demortgagiblePropertiesOfOwners;
    public IPropertyData? PropertyToDeMortgage => this.propertyToDemortgage;
    public int? CurrentPlayerToDemortgage => this.currentPlayerToDemortgage;
    public List<IPropertyData> DeMortgagiblePropertiesOfCurrentPlayer => this.demortgagiblePropertiesOfOwners[(int)this.currentPlayerToDemortgage!];

    public void SetDeMortgageHandler(List<int> balances, List<IPropertyData> properties)
    {
        this.playerChangedCount = 0;
        this.propertyToDemortgage = null;
        this.backupPropertyDatas = properties;
        this.backupBalances = balances;
        this.SetAreAnyDemortgagible();
        if (this.areAnyDemortgagible is true)
        {
            this.SetDeMortgagiblePropertiesOfOwners();
            this.SetParticipantPlayerNumbers();
            this.currentPlayerToDemortgage = this.participantPlayerNumbers![this.playerChangedCount];
        }
    }

    public void ChangePlayerToDeMortgage()
    {
        if (this.IsLastPlayer)
        {
            throw new Exception();
        }

        this.propertyToDemortgage = null;
        this.playerChangedCount++;
        this.currentPlayerToDemortgage = this.participantPlayerNumbers![(int)this.playerChangedCount!];
    }

    public void SetRealEstateToBuildHouse(int indexOfRealEstate)
    {
        this.propertyToDemortgage = DeMortgagiblePropertiesOfCurrentPlayer[indexOfRealEstate];
    }

    private void SetDeMortgagiblePropertiesOfOwners()
    {
        List<IPropertyData> buildableRealEstateDatas = this.CreateReallyDemortgagibleProperties();
        this.demortgagiblePropertiesOfOwners.Clear();

        foreach (var property in buildableRealEstateDatas)
        {
            int ownerNumber = (int)property.OwnerPlayerNumber!;

            if (this.demortgagiblePropertiesOfOwners.Keys.Contains(ownerNumber) is false)
            {
                this.demortgagiblePropertiesOfOwners.Add(ownerNumber, new List<IPropertyData>());
            }

            this.demortgagiblePropertiesOfOwners[ownerNumber].Add(property);
        }
    }

    private void SetAreAnyDemortgagible()
    {
        List<IPropertyData> demortgagibleProperties = this.backupPropertyDatas!
                                                        .Where(property => property.IsMortgaged)
                                                        .ToList();
        
        foreach (var property in demortgagibleProperties)
        {
            int ownerNumber = (int)property.OwnerPlayerNumber!;

            if (this.backupBalances![ownerNumber] >= 1.1 * property.Mortgage)
            {
                this.areAnyDemortgagible = true;
                return;
            }
        }

        this.areAnyDemortgagible = false;
        return;
    }

    private List<IPropertyData> CreateReallyDemortgagibleProperties()
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
        this.participantPlayerNumbers = this.demortgagiblePropertiesOfOwners.Keys.ToList();
        this.participantPlayerNumbers.Sort();
    }
}
