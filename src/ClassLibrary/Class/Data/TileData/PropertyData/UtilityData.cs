
public class UtilityData : PropertyData
{
    private Utility utility;

    public UtilityData(Property property) : base(property)
    {
        this.utility = (Utility) property;
    }
}
