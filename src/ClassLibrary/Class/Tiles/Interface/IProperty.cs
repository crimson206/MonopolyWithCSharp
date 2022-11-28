public interface IProperty : IPropertyData
{
    public void SetOwnerPlayerNumber(int? playerNumber);

    public void SetIsMortgaged(bool isMortgaged);

    public void SetGroup(List<IProperty> group);


}