public class TradeHandler : ITradeHandlerFunction, ITradeHandlerData
{
    private List<int> participantNumbers = new List<int>();
    private int tradeCount = 0;
    private int? currentTradeOwner;
    private int? currentTradeTarget;
    private IPropertyData? propertyTradeOwnerToGet;
    private IPropertyData? propertyTradeOwnerToGive;
    private int moneyOwnerWillingToAddOnTrade;
    private TileFilter tileFilter = new TileFilter();
    private Dictionary<int, List<IPropertyData>>? ownedTradablePropertyDatas;
    private bool? isTradeAgreed;
    private bool isLastParticipant = false;
    private List<Property> backUpProperties = new List<Property>();

    public List<IPropertyData> TradablePropertiesOfTradeOwner =>
        this.CreateTradablePropertiesOfTradeOwner();

    public List<IPropertyData> TradablePropertiesOfTradeTarget =>
        this.CreateTradablePropertiesOfTradeTarget();

    public IPropertyData? PropertyTradeOwnerToGet => this.propertyTradeOwnerToGet;

    public IPropertyData? PropertyTradeOwnerToGive => this.propertyTradeOwnerToGive;

    public int MoneyOwnerWillingToAddOnTrade => this.moneyOwnerWillingToAddOnTrade;

    public bool? IsTradeAgreed => this.isTradeAgreed;

    public int? CurrentTradeOwner => this.currentTradeOwner;

    public int? CurrentTradeTarget => this.currentTradeTarget;

    public bool IsLastParticipant => this.isLastParticipant;

    public List<int> SelectableTargetNumbers => this.CreateSelectableTargetNumbers();

    public bool IsThereTradableProperties => this.CheckIfThereIsAnyTradableProperty();

    public void SetTrade(
        List<int> participantNumbers,
        List<Property> properties)
    {
        this.ResetInitialTradeConditions();
        this.ResetTradeConditionMeanwhile();

        this.participantNumbers = participantNumbers;
        this.backUpProperties = properties;
        this.currentTradeOwner = participantNumbers[0];
        this.SetTradablePropertyDatas();
    }

    public void SetTradeTarget(
        int tradeTarget)
    {
        if (this.SelectableTargetNumbers.Contains(tradeTarget))
        {
            this.currentTradeTarget = tradeTarget;
        }
        else
        {
            throw new Exception();
        }
    }

    public void SuggestTradeConditions(
        IPropertyData? propertyOwnerWantsFromTarget,
        IPropertyData? propertyOwnerIsWillingToExchange,
        int moneyOwnerWillingToAddOnTrade)
    {
        this.propertyTradeOwnerToGet = propertyOwnerWantsFromTarget;
        this.propertyTradeOwnerToGive = propertyOwnerIsWillingToExchange;
        this.moneyOwnerWillingToAddOnTrade = moneyOwnerWillingToAddOnTrade;
    }

    public void SetIsTradeAgreed(
        bool agreed)
    {
        if (this.propertyTradeOwnerToGet is null
            && this.propertyTradeOwnerToGive is null)
        {
            throw new Exception();
        }

        this.isTradeAgreed = agreed;
    }

    public void ChangeTradeOwner()
    {
        if (this.isLastParticipant)
        {
            throw new Exception();
        }

        this.ResetTradeConditionMeanwhile();

        this.tradeCount++;
        this.currentTradeOwner = this.participantNumbers[this.tradeCount];
        this.SetTradablePropertyDatas();

        if (this.tradeCount == this.participantNumbers.Count() - 1)
        {
            this.isLastParticipant = true;
        }
    }

    private void ResetInitialTradeConditions()
    {
        this.tradeCount = 0;
        this.isLastParticipant = false;
    }

    private void ResetTradeConditionMeanwhile()
    {
        this.propertyTradeOwnerToGive = null;
        this.propertyTradeOwnerToGet = null;
        this.moneyOwnerWillingToAddOnTrade = 0;
        this.isTradeAgreed = null;
        this.ownedTradablePropertyDatas = null;    
        this.currentTradeTarget = null;
    }

    private List<int> CreateSelectableTargetNumbers()
    {
        List<int> selectableTargetNumbers = new List<int>();

        foreach (var participantNumber in participantNumbers)
        {
            if (participantNumber != this.CurrentTradeOwner
                && this.ownedTradablePropertyDatas![participantNumber].Count() != 0)
            {
                selectableTargetNumbers.Add(participantNumber);
            }
        }

        return selectableTargetNumbers;
    }

    private bool CheckIfThereIsAnyTradableProperty()
    {
        bool isThereTradableProperties =
            this.backUpProperties
            .Any(property => property.IsTradable is true);

        return isThereTradableProperties;
    }


    private void SetTradablePropertyDatas()
    {
        List<Property> tradableProperties =
            this.backUpProperties.
            Where(property => property.IsTradable is true).
            ToList();

        this.ownedTradablePropertyDatas =
            this.tileFilter.
            ConvertPropertiesToOwnedPropertyDatasDictionary(this.participantNumbers, tradableProperties);
    }

    private List<IPropertyData> CreateTradablePropertiesOfTradeOwner()
    {
        if (this.currentTradeOwner is not null)
        {
            int tradeOwner = (int)this.currentTradeOwner;
            return this.ownedTradablePropertyDatas![tradeOwner];
        }
        else
        {
            return new List<IPropertyData>();
        }
    }

    private List<IPropertyData> CreateTradablePropertiesOfTradeTarget()
    {
        if (this.currentTradeTarget is not null)
        {
            int tradeTarget = (int)this.currentTradeTarget;
            return this.ownedTradablePropertyDatas![tradeTarget];
        }
        else
        {
            return new List<IPropertyData>();
        }
    }
}
