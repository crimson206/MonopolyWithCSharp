public interface ITradeHandlerData
{
    public List<IPropertyData> TradablePropertiesOfTradeOwner { get; }
    public List<IPropertyData> TradablePropertiesOfTradeTarget { get; }
    public IPropertyData? PropertyTradeOwnerToGet { get; }
    public IPropertyData? PropertyTradeOwnerToGive { get; }
    public int MoneyOwnerWillingToAddOnTrade { get; }
    public bool? IsTradeAgreed { get; }
    public int? CurrentTradeOwner { get; }
    public int? CurrentTradeTarget { get; }
    public bool IsTimeToCloseTrade { get; }
}