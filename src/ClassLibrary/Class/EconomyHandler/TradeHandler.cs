public class TradeHandler
{
    private List<int> participantNumbers = new List<int>();
    private int tradeCount = 0;
    private int currentTradeOwner;
    private int currentTradeTarget;
    private IPropertyData? propertyOwnerWantsFromTarget;
    private IPropertyData? propertyOwnerIsWillingToExchange;
    private int moneyOwnerWillingToAddOnTrade;
    private TileFilter tileFilter = new TileFilter();
    private Dictionary<int, List<IPropertyData>>? ownedTradablePropertyDatas;
    private bool? isTradeAgreed;
    private bool didAllParticipantsTry = false;

    public List<IPropertyData> TradeOwnersTradableProperties =>
        this.ownedTradablePropertyDatas![this.currentTradeOwner];
    public List<IPropertyData> TradeTargetsTradableProperties =>
        this.ownedTradablePropertyDatas![this.currentTradeTarget];
    public IPropertyData? PropertyOwnerWantsFromTarget => this.propertyOwnerWantsFromTarget;
    public IPropertyData? PropertyOwnerIsWillingToExchange => this.propertyOwnerIsWillingToExchange;
    public int MoneyOwnerWillingToAddOnTrade => this.moneyOwnerWillingToAddOnTrade;
    public bool? IsTradeAgreed => this.isTradeAgreed;

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
        int currentTradeTarget
    )
    {
        this.currentTradeTarget = currentTradeTarget;
    }

    public void SetTradeCondition(
        IPropertyData? propertyOwnerWantsFromTarget,
        IPropertyData? propertyOwnerIsWillingToExchange,
        int moneyOwnerWillingToAddOnTrade)
    {
        this.propertyOwnerWantsFromTarget = propertyOwnerWantsFromTarget;
        this.propertyOwnerIsWillingToExchange = propertyOwnerIsWillingToExchange;
        this.moneyOwnerWillingToAddOnTrade = moneyOwnerWillingToAddOnTrade;
    }

    public void SetTradeTargetIsTradeAgreed(bool agreed)
    {
        this.isTradeAgreed = agreed;
        this.tradeCount++;

        if (this.tradeCount == this.participantNumbers.Count())
        {
            this.didAllParticipantsTry = true;
        }
    }

    public void ChangeTradeOwner(List<Property> properties)
    {
        if (this.didAllParticipantsTry)
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
        this.didAllParticipantsTry = false;
        this.propertyOwnerIsWillingToExchange = null;
        this.propertyOwnerWantsFromTarget = null;
        this.isTradeAgreed = null;
        this.ownedTradablePropertyDatas = null;
    }

    private void ResetTradeConditionSoft()
    {
        this.propertyOwnerIsWillingToExchange = null;
        this.propertyOwnerWantsFromTarget = null;
        this.isTradeAgreed = null;
        this.ownedTradablePropertyDatas = null;      
    }
}
