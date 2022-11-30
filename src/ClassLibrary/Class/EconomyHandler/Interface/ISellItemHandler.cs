public interface ISellItemHandler : ISellItemHandlerData
{
    public void SetPlayerToSellItems(int playerNumber, List<IPropertyData> properties);
    public void SetSellingOption(Dictionary<SellingType, int> itemToSell);
    public void ResetPropertyToChange();
}