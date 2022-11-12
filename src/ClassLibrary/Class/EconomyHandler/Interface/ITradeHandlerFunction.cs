public interface ITradeHandlerFunction
{
    public void SetTrade(List<int> participantNumbers, List<Property> properties);

    public void SetTradeTarget(int tradeTarget);

    public void SuggestTradeOwnerTradeCondition(
        IPropertyData? propertyOwnerWantsFromTarget,
        IPropertyData? propertyOwnerIsWillingToExchange,
        int moneyOwnerWillingToAddOnTrade);

    public void MakeTradeTargetDecionOnTradeAgreement(bool agreed);

    public void ChangeTradeOwner(List<Property> properties);
}