public class PropertyManager : IPropertyManager
{
    public void ChangeOwner(IProperty property, int? playerNumber)
    {
        property.SetOwnerPlayerNumber(playerNumber);
    }

    public void ChangeOwner(IPropertyData propertyData, int? playerNumber)
    {
        IProperty property = (IProperty)propertyData;
        property.SetOwnerPlayerNumber(playerNumber);
    }

    public void SetIsMortgaged(IProperty property, bool isMortgaged)
    {
        property.SetIsMortgaged(isMortgaged);
    }

    public void SetIsMortgaged(IPropertyData propertyData, bool isMortgaged)
    {   
        IProperty property = (IProperty)propertyData;
        property.SetIsMortgaged(isMortgaged);
    }

    public void BuildHouse(IRealEstate realEstate)
    {
        realEstate.BuildHouse();
    }

    public void DistructHouse(IRealEstate realEstate)
    {
        realEstate.DistructHouse();
    }
}
