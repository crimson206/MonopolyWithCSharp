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
    private bool hadAllParticipantsTheirTurn = false;

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
    public bool HadAllParticipantsTheirTurn => this.hadAllParticipantsTheirTurn;

    public void SetTrade(List<int> participantNumbers, List<Property> properties)
    {
        this.ResetTradeConditionsHard();

        this.participantNumbers = participantNumbers;
        this.currentTradeOwner = this.participantNumbers[this.tradeCount];
        List<Property> tradableProperties =
            properties.
            Where(property => property.IsTradable is true).
            ToList();

        this.ownedTradablePropertyDatas =
            this.tileFilter.
            ConvertPropertiesToOwnedPropertyDatasDictionary(tradableProperties);
    }

    public void SetTradeTarget(
        int tradeTarget
    )
    {
        this.currentTradeTarget = tradeTarget;
    }

    public void SuggestTradeOwnerTradeCondition(
        IPropertyData? propertyOwnerWantsFromTarget,
        IPropertyData? propertyOwnerIsWillingToExchange,
        int moneyOwnerWillingToAddOnTrade)
    {
        this.propertyTradeOwnerToGet = propertyOwnerWantsFromTarget;
        this.propertyTradeOwnerToGive = propertyOwnerIsWillingToExchange;
        this.moneyOwnerWillingToAddOnTrade = moneyOwnerWillingToAddOnTrade;
    }

    public void MakeTradeTargetDecionOnTradeAgreement(bool agreed)
    {
        this.isTradeAgreed = agreed;
        this.tradeCount++;

        if (this.tradeCount == this.participantNumbers.Count())
        {
            this.hadAllParticipantsTheirTurn = true;
        }
    }

    public void ChangeTradeOwner(List<Property> properties)
    {
        if (this.hadAllParticipantsTheirTurn)
        {
            throw new Exception();
        }

        this.ResetTradeConditionSoft();

        this.currentTradeOwner = this.participantNumbers[this.tradeCount];

        List<Property> tradableProperties =
            properties.
            Where(property => property.IsTradable is true).
            ToList();

        this.ownedTradablePropertyDatas =
            this.tileFilter.
            ConvertPropertiesToOwnedPropertyDatasDictionary(tradableProperties);
    }

    private void ResetTradeConditionsHard()
    {
        this.tradeCount = 0;
        this.hadAllParticipantsTheirTurn = false;
        this.propertyTradeOwnerToGive = null;
        this.propertyTradeOwnerToGet = null;
        this.isTradeAgreed = null;
        this.ownedTradablePropertyDatas = null;
    }

    private void ResetTradeConditionSoft()
    {
        this.propertyTradeOwnerToGive = null;
        this.propertyTradeOwnerToGet = null;
        this.isTradeAgreed = null;
        this.ownedTradablePropertyDatas = null;      
    }
}
