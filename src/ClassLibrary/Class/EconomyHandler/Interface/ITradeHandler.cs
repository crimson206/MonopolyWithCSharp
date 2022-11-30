public interface ITradeHandler : ITradeHandlerData
{
    public void SetTrade(List<int> participantNumbers, List<IProperty> properties);

    public void SetTradeTarget(int tradeTarget);

    public void SetPropertyTradeOwnerWantsFromTarget(int indexFromTradablePropertiesOfTradeOwner);

    public void SetPropertyTradeOwnerIsWillingToGive(int indexFromTradablePropertiesOfTradeTarget);

    public void SetAdditionalMoneyTradeOwnerIsWillingToAdd(int moneyOwnerWillingToAddOnTrade);

    public void SetIsTradeAgreed(bool agreed);

    public void ChangeTradeOwner();

}