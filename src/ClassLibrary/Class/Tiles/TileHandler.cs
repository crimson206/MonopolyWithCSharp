using Newtonsoft.Json.Linq;
public class TileHandler : ITileFactory
{
    private List<string> tileTypeList = new List<string>
    {
        "Go", "GoToJail", "CommunityChest", "Chance", "FreeParking",
        "IncomeTax", "Jail", "Utility", "RailRoad", "RealEstate", "LuxuryTax"
    };

    public Go CreateGo(JToken jGo)
    {
        int position = jGo["position"]!.Value<int>();
        string name = jGo["name"]!.ToString();
        int salary = jGo["salary"]!.Value<int>();

        return new Go(position, name, salary);
    }
    public Jail CreateJail(JToken jJail)
    {
        int position = jJail["position"]!.Value<int>();
        string name = jJail["name"]!.ToString();
        int jailFine = jJail["jailFine"]!.Value<int>();

        return new Jail(position, name, jailFine);
    }
    public RealEstate CreateRealEstate(JToken jRealEstate)
    {
        int position = jRealEstate["position"]!.Value<int>();
        string name = jRealEstate["name"]!.ToString();
        int price = jRealEstate["price"]!.Value<int>();
        List<int> rents = jRealEstate["rents"]!.ToObject<List<int>>()!;
        int mortgageValue = jRealEstate["mortgageValue"]!.Value<int>();
        string color = jRealEstate["color"]!.ToString();

        return new RealEstate(position, name, price, rents, mortgageValue, color);
    }
    public GoToJail CreateGoToJail(JToken jGoToJail)
    {
        int position = jGoToJail["position"]!.Value<int>();
        string name = jGoToJail["name"]!.ToString();

        return new GoToJail(position, name);
    }
    public CommunityChest CreateCommunityChest(JToken jCommunityChest)
    {
        int position = jCommunityChest["position"]!.Value<int>();
        string name = jCommunityChest["name"]!.ToString();
        return new CommunityChest(position, name);
    }
    public Chance CreateChance(JToken jChance)
    {
        int position = jChance["position"]!.Value<int>();
        string name = jChance["name"]!.ToString();

        return new Chance(position, name);
    }
    public FreeParking CreateFreeParking(JToken jFreeParking)
    {
        int position = jFreeParking["position"]!.Value<int>();
        string name = jFreeParking["name"]!.ToString();
        return new FreeParking(position, name);
    }
    public IncomeTax CreateIncomeTax(JToken jIncomeTax)
    {
        int position = jIncomeTax["position"]!.Value<int>();
        string name = jIncomeTax["name"]!.ToString();
        int tax = jIncomeTax["tax"]!.Value<int>();
        int percentageTax = jIncomeTax["percentageTax"]!.Value<int>();
        return new IncomeTax(position, name, tax, percentageTax);
    }
    public Utility CreateUtility(JToken jUtility)
    {
        int position = jUtility["position"]!.Value<int>();
        string name = jUtility["name"]!.ToString();
        int price = jUtility["price"]!.Value<int>();
        List<int> rents = jUtility["rents"]!.ToObject<List<int>>()!;
        int mortgageValue = jUtility["mortgageValue"]!.Value<int>();
        return new Utility(position, name, price, rents, mortgageValue);
    }
    public RailRoad CreateRailRoad(JToken jRailRoad)
    {
        int position = jRailRoad["position"]!.Value<int>();
        string name = jRailRoad["name"]!.ToString();
        int price = jRailRoad["price"]!.Value<int>();
        List<int> rents = jRailRoad["rents"]!.ToObject<List<int>>()!;
        int mortgageValue = jRailRoad["mortgageValue"]!.Value<int>();
        return new RailRoad(position, name, price, rents, mortgageValue);
    }
    public LuxuryTax CreateLuxuryTax(JToken jLuxuryTax)
    {
        int position = jLuxuryTax["position"]!.Value<int>();
        string name = jLuxuryTax["name"]!.ToString();
        int tax = jLuxuryTax["tax"]!.Value<int>();
        return new LuxuryTax(position, name, tax);
    }


    public List<Tile> CreateTiles(StreamReader readTile)
    {
        List<Tile> tiles = new List<Tile>();
        string jStringTiles = readTile.ReadToEnd();
        JObject jObjectTiles = JObject.Parse(jStringTiles);
        List<JToken> jTokenTiles = jObjectTiles.Values().ToList();
        
        foreach (var jTokenTile in jTokenTiles)
        {
            if(jTokenTile["type"]!.ToString()=="Go")
            {
                Go go = CreateGo(jTokenTile);
                tiles.Add(go);
            }
            else if(jTokenTile["type"]!.ToString()=="GoToJail")
            {
                GoToJail goToJail = CreateGoToJail(jTokenTile);
                tiles.Add(goToJail);
            }
            else if(jTokenTile["type"]!.ToString()=="CommunityChest")
            {
                CommunityChest communityChest = CreateCommunityChest(jTokenTile);
                tiles.Add(communityChest);
            }
            else if(jTokenTile["type"]!.ToString()=="Chance")
            {
                Chance chance = CreateChance(jTokenTile);
                tiles.Add(chance);
            }
            else if(jTokenTile["type"]!.ToString()=="FreeParking")
            {
                FreeParking freeParking = CreateFreeParking(jTokenTile);
                tiles.Add(freeParking);
            }
            else if(jTokenTile["type"]!.ToString()=="IncomeTax")
            {
                IncomeTax incomeTax = CreateIncomeTax(jTokenTile);
                tiles.Add(incomeTax);
            }
            else if(jTokenTile["type"]!.ToString()=="Jail")
            {
                Jail jail = CreateJail(jTokenTile);
                tiles.Add(jail);
            }
            else if(jTokenTile["type"]!.ToString()=="Utility")
            {
                Utility utility = CreateUtility(jTokenTile);
                tiles.Add(utility);
            }
            else if(jTokenTile["type"]!.ToString()=="RailRoad")
            {
                RailRoad railRoad = CreateRailRoad(jTokenTile);
                tiles.Add(railRoad);
            }
            else if(jTokenTile["type"]!.ToString()=="RealEstate")
            {
                RealEstate realEstate = CreateRealEstate(jTokenTile);
                tiles.Add(realEstate);
            }
            else if(jTokenTile["type"]!.ToString()=="LuxuryTax")
            {
                LuxuryTax luxuryTax = CreateLuxuryTax(jTokenTile);
                tiles.Add(luxuryTax);
            }
        }

        return tiles;

    }
}


///        "Go", "GoToJail", "CommunityChest", "Chance", "FreeParking",
///        "IncomeTax", "Jail", "Utility", "RailRoad", "RealEstate", "LuxuryTax"

