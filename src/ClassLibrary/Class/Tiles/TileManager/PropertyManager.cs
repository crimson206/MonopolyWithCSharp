public class PropertyManager
{
    public void ChangeOwner(Property property, int? playerNumber)
    {
        property.SetOnwerPlayerNumber(playerNumber);
    }

    public void SetIsMortgaged(Property property, bool isMortgaged)
    {
        property.SetIsMortgaged(isMortgaged);
    }

    public void BuildHouse(RealEstate realEstate)
    {
        realEstate.BuildHouse();
    }

    public void DistructHouse(RealEstate realEstate)
    {
        realEstate.DistructHouse();
    }
}
