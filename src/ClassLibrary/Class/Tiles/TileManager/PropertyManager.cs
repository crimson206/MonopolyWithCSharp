
public class PropertyManager
{
    private int password;

    public PropertyManager(int password)
    {
        this.password = password;
    }

    public void ChangeOwner(Property property, int? playerNumber)
    {
        property.SetOnwerPlayerNumber(this.password, playerNumber);
    }
    public void SetIsMortgaged(Property property, bool isMortgaged)
    {
        property.SetIsMortgaged(password, isMortgaged);
    }
    public void BuildHouse(RealEstate realEstate)
    {
        realEstate.BuildHouse(this.password);
    }
    public void DistructHouse(RealEstate realEstate)
    {
        realEstate.DistructHouse(this.password);
    }
}
