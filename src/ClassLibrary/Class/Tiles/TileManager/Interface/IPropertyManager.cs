public interface IPropertyManager
{
    public void ChangeOwner(IProperty property, int? playerNumber);
    public void ChangeOwner(IPropertyData property, int? playerNumber);
    public void SetIsMortgaged(IProperty property, bool isMortgaged);

    public void BuildHouse(IRealEstate realEstate);

    public void DistructHouse(IRealEstate realEstate);

    public void SetIsMortgaged(IPropertyData propertyData, bool isMortgaged);


}