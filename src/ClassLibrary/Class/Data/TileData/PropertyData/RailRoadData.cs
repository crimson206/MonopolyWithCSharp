
public class RailRoadData : PropertyData
{
    private RailRoad railRoad;

    public RailRoadData(Property property) : base(property)
    {
        this.railRoad = (RailRoad) property;
    }
}
