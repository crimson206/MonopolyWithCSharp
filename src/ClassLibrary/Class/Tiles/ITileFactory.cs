using Newtonsoft.Json.Linq;

public interface ITileFactory
{
    protected abstract Go CreateGo(JToken jGo);
    protected abstract GoToJail CreateGoToJail(JToken jGoToJail);
    protected abstract CommunityChest CreateCommunityChest(JToken jCommunityChest);
    protected abstract Chance CreateChance(JToken jChance);
    protected abstract FreeParking CreateFreeParking(JToken jFreeParking);
    protected abstract IncomeTax CreateIncomeTax(JToken jIncomeTax);
    protected abstract Jail CreateJail(JToken jJail);

    protected abstract Utility CreateUtility(JToken jUtility);
    protected abstract RailRoad CreateRailRoad(JToken jRailRoad);
    protected abstract RealEstate CreateRealEstate(JToken jRealEstate);

    protected abstract LuxuryTax CreateLuxuryTax(JToken jLuxuryTax);
}

