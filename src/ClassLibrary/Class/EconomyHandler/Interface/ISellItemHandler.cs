public interface ISellItemHandler : ISellItemHandlerData
{
    public void SetPlayerToSellItems(int playerNumber, List<IPropertyData> properties);
    public void SetPropertyToAuction(int index);

    public void SetRealEstateToBuildHouse(int index);

    public void SetPropertyToMortgage(int index);

    public void ResetPropertyToChange();
}