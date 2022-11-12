public class TradeHandler : ITradeHandlerFunction, ITradeHandlerData
{
    private List<int> participantNumbers = new List<int>();
    private int tradeCount = 0;
    private int currentTradeOwner;
    private int currentTradeTarget;
    private IPropertyData? propertyTradeOwnerToGet;
    private IPropertyData? propertyTradeOwnerToGive;
    private int moneyOwnerWillingToAddOnTrade;
    private TileFilter tileFilter = new TileFilter();
    private Dictionary<int, List<IPropertyData>>? ownedTradablePropertyDatas;
    private bool? isTradeAgreed;
    private bool isTimeToCloseTrade = false;
    private List<Property> backUpProperties = new List<Property>();

    public List<IPropertyData> TradablePropertiesOfTradeOwner =>
        this.ownedTradablePropertyDatas![this.currentTradeOwner];
    public List<IPropertyData> TradablePropertiesOfTradeTarget =>
        this.ownedTradablePropertyDatas![this.currentTradeTarget];
    public IPropertyData? PropertyTradeOwnerToGet => this.propertyTradeOwnerToGet;
    public IPropertyData? PropertyTradeOwnerToGive => this.propertyTradeOwnerToGive;
    public int MoneyOwnerWillingToAddOnTrade => this.moneyOwnerWillingToAddOnTrade;
    public bool? IsTradeAgreed => this.isTradeAgreed;
    public int CurrentTradeOwner => this.currentTradeOwner;
    public int CurrentTradeTarget => this.currentTradeTarget;
    public bool IsTradeCountEqualToParticipantCount => this.isTimeToCloseTrade;

    public List<int> SelectableTargetNumbers => this.CreateSelectableTargetNumbers(this.currentTradeOwner);

    public void SetTrade(
        List<int> participantNumbers,
        List<Property> properties)
    {
        this.ResetInitialTradeConditions();

        this.participantNumbers = participantNumbers;
        this.backUpProperties = properties;
        this.currentTradeOwner = participantNumbers[0];
        this.SetTradablePropertyDatas();

        if (this.CheckIfThereIsAnyTradableProperty(properties) is false)
        {
            this.isTimeToCloseTrade = true;
        }
        else if (this.SelectableTargetNumbers.Count() != 0)
        {
            return;
        }
        else
        {
            this.ChangeTradeOwner();
        }
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
        if (this.isTimeToCloseTrade)
        {
            throw new Exception();
        }

        this.ResetTradeConditionMeanwhile();

        do
        {
            this.tradeCount++;
            this.currentTradeOwner = this.participantNumbers[this.tradeCount];
            this.SetTradablePropertyDatas();
        } while (this.SelectableTargetNumbers.Count() == 0);

        if (this.tradeCount == this.participantNumbers.Count() - 1)
        {
            this.isTimeToCloseTrade = true;
        }

    }

    private void ResetInitialTradeConditions()
    {
        this.tradeCount = 0;
        this.isTimeToCloseTrade = false;
    }

    private void ResetTradeConditionMeanwhile()
    {
        this.propertyTradeOwnerToGive = null;
        this.propertyTradeOwnerToGet = null;
        this.isTradeAgreed = null;
        this.ownedTradablePropertyDatas = null;      
    }

    private List<int> CreateSelectableTargetNumbers(int tradeOwner)
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

    private bool CheckIfThereIsAnyTradableProperty(
        List<Property> properties)
    {
        bool isThereTradableProperties =
            properties
            .Any(property => property.IsTradable is true);

        return isThereTradableProperties;
    }

    private bool CheckIfTradeOwnerIsChangible()
    {
        int participantCount =
            this.participantNumbers
                .Count();

        if (this.tradeCount < participantCount)
        {
            /// i is the remaind participants indeces
            for (int i = tradeCount; i < participantCount; i++)
            {
                if( this.CreateSelectableTargetNumbers(i).Count() != 0)
                {
                    return true;
                }
            }
        }

        return false;
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
}
