public interface ITradeHandlerFunction
{
    public void SetTrade(List<int> participantNumbers, List<Property> properties);

    public void SetTradeTarget(int tradeTarget);

    public void SuggestTradeConditions(
        IPropertyData? propertyOwnerWantsFromTarget,
        IPropertyData? propertyOwnerIsWillingToExchange,
        int moneyOwnerWillingToAddOnTrade);

    public void SetIsTradeAgreed(bool agreed);

    public void ChangeTradeOwner();
}