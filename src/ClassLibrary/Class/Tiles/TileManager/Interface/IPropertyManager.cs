public interface IPropertyManager
{
    public void ChangeOwner(Property property, int? playerNumber);

    public void SetIsMortgaged(Property property, bool isMortgaged);

    public void BuildHouse(RealEstate realEstate);

    public void DistructHouse(RealEstate realEstate);

    public void SetIsMortgaged(IPropertyData propertyData, bool value);

    public void DistructHouse(IPropertyData propertyData, bool value);


}