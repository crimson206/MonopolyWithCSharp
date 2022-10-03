//-----------------------------------------------------------------------
// <copyright file="JsonTileConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Newtonsoft.Json.Linq;

/// <summary>
/// This class can create a list of tiles from a certain form of Json string.
/// Try ShowExampleJson() to check the form.
/// </summary>
public class JsonTileConverter
{
    /// <summary>
    /// This method creates a list of tile objects by converting a Json string.
    /// </summary>
    /// <param name="readTiles">The json string to convert</param>
    /// <returns>The list of tiles which arc converted from a certain form of Json string</returns>
    public List<Tile> CreateTiles2(StreamReader readTiles)
    {
        List<Tile> tiles = new List<Tile>();
        string jStringTiles = readTiles.ReadToEnd();
        JObject jObjectTiles = JObject.Parse(jStringTiles);
        List<JToken> jTokenTiles = jObjectTiles.Values().ToList();

        Dictionary<string, Func<JToken, Tile>> creatorDictionary = CreateCreatorDictionary();

        foreach (var jTokenTile in jTokenTiles)
        {
            string tileType =jTokenTile["type"]!.ToString();
            Tile newTile = creatorDictionary[tileType](jTokenTile);
            tiles.Add(newTile);
        }
        return tiles;
    }

    /// <summary>
    /// This method writes the example json string on the Console.
    /// </summary>
    public void ShowExampleJson()
    {
        var exampleJson = @"{
    '0': {
        'position': 1,
        'name': 'Mediterranean Avenue',
        'type': 'RealEstate',
        'color': 'Brown',
        'price': 60,
        'rents': [2, 4, 10, 30, 90, 160, 250],
        'mortgageValue': 30,
        'buildingCost': 50
    },
    '1': {
        'position': 5,
        'name': 'Reading Railroad',
        'type': 'RailRoad',
        'price': 200,
        'rents': [25, 50, 100, 200],
        'mortgageValue': 100
    },
    '2': {
        'position': 12,
        'name': 'Electric Company',
        'type': 'Utility',
        'price': 150,
        'rents': [4, 10],
        'mortgageValue': 75
    },
    '3': {
        'position': 10,
        'name': 'Jail',
        'type': 'Jail',
        'jailFine': 50
    },
    '4': {
        'position': 0,
        'name': 'Go!',
        'type': 'Go',
        'salary': 200
    },
    '5': {
        'position': 4,
        'name': 'Income Tax',
        'type': 'IncomeTax',
        'tax': 200,
        'percentageTax': 10
    },
    '6': {
        'position': 20,
        'name': 'Free Parking',
        'type': 'FreeParking'
    },
    '7': {
        'position': 7,
        'name': 'Chance',
        'type': 'Chance'
    },
    '8': {
        'position': 2,
        'name': 'Community Chest',
        'type': 'CommunityChest'
    },
    '9': {
        'position': 30,
        'name': 'Go To Jail',
        'type': 'GoToJail'
    },
    '10': {
        'position': 38,
        'name': 'Luxury Tax',
        'type': 'LuxuryTax',
        'tax': 100
    }
}
";
        Console.WriteLine(exampleJson);
    }


    private Go CreateGo(JToken jGo)
    {
        int position = jGo["position"]!.Value<int>();
        string name = jGo["name"]!.ToString();
        int salary = jGo["salary"]!.Value<int>();

        return new Go(position, name, salary);
    }
    private Jail CreateJail(JToken jJail)
    {
        int position = jJail["position"]!.Value<int>();
        string name = jJail["name"]!.ToString();
        int jailFine = jJail["jailFine"]!.Value<int>();

        return new Jail(position, name, jailFine);
    }
    private RealEstate CreateRealEstate(JToken jRealEstate)
    {
        int position = jRealEstate["position"]!.Value<int>();
        string name = jRealEstate["name"]!.ToString();
        int price = jRealEstate["price"]!.Value<int>();
        List<int> rents = jRealEstate["rents"]!.ToObject<List<int>>()!;
        int mortgageValue = jRealEstate["mortgageValue"]!.Value<int>();
        string color = jRealEstate["color"]!.ToString();

        return new RealEstate(position, name, price, rents, mortgageValue, color);
    }
    private GoToJail CreateGoToJail(JToken jGoToJail)
    {
        int position = jGoToJail["position"]!.Value<int>();
        string name = jGoToJail["name"]!.ToString();

        return new GoToJail(position, name);
    }
    private CommunityChest CreateCommunityChest(JToken jCommunityChest)
    {
        int position = jCommunityChest["position"]!.Value<int>();
        string name = jCommunityChest["name"]!.ToString();
        return new CommunityChest(position, name);
    }
    private Chance CreateChance(JToken jChance)
    {
        int position = jChance["position"]!.Value<int>();
        string name = jChance["name"]!.ToString();

        return new Chance(position, name);
    }
    private FreeParking CreateFreeParking(JToken jFreeParking)
    {
        int position = jFreeParking["position"]!.Value<int>();
        string name = jFreeParking["name"]!.ToString();
        return new FreeParking(position, name);
    }
    private IncomeTax CreateIncomeTax(JToken jIncomeTax)
    {
        int position = jIncomeTax["position"]!.Value<int>();
        string name = jIncomeTax["name"]!.ToString();
        int tax = jIncomeTax["tax"]!.Value<int>();
        int percentageTax = jIncomeTax["percentageTax"]!.Value<int>();
        return new IncomeTax(position, name, tax, percentageTax);
    }
    private Utility CreateUtility(JToken jUtility)
    {
        int position = jUtility["position"]!.Value<int>();
        string name = jUtility["name"]!.ToString();
        int price = jUtility["price"]!.Value<int>();
        List<int> rents = jUtility["rents"]!.ToObject<List<int>>()!;
        int mortgageValue = jUtility["mortgageValue"]!.Value<int>();
        return new Utility(position, name, price, rents, mortgageValue);
    }
    private RailRoad CreateRailRoad(JToken jRailRoad)
    {
        int position = jRailRoad["position"]!.Value<int>();
        string name = jRailRoad["name"]!.ToString();
        int price = jRailRoad["price"]!.Value<int>();
        List<int> rents = jRailRoad["rents"]!.ToObject<List<int>>()!;
        int mortgageValue = jRailRoad["mortgageValue"]!.Value<int>();
        return new RailRoad(position, name, price, rents, mortgageValue);
    }
    private LuxuryTax CreateLuxuryTax(JToken jLuxuryTax)
    {
        int position = jLuxuryTax["position"]!.Value<int>();
        string name = jLuxuryTax["name"]!.ToString();
        int tax = jLuxuryTax["tax"]!.Value<int>();
        return new LuxuryTax(position, name, tax);
    }

    private List<string> tileTypeList = new List<string>
    {
        "Go", "GoToJail", "CommunityChest", "Chance", "FreeParking",
        "IncomeTax", "Jail", "Utility", "RailRoad", "RealEstate", "LuxuryTax"
    };

    private Dictionary<string, Func<JToken, Tile>> CreateCreatorDictionary()
    {
        Dictionary<string, Func<JToken, Tile>> creatorDictionary = new Dictionary<string, Func<JToken, Tile>>();
        creatorDictionary.Add("Go", CreateGo);
        creatorDictionary.Add("GoToJail", CreateGoToJail);
        creatorDictionary.Add("CommunityChest", CreateCommunityChest);
        creatorDictionary.Add("Chance", CreateChance);
        creatorDictionary.Add("FreeParking", CreateFreeParking);
        creatorDictionary.Add("IncomeTax", CreateIncomeTax);
        creatorDictionary.Add("Jail", CreateJail);
        creatorDictionary.Add("Utility", CreateUtility);
        creatorDictionary.Add("RailRoad", CreateRailRoad);
        creatorDictionary.Add("RealEstate", CreateRealEstate);
        creatorDictionary.Add("LuxuryTax", CreateLuxuryTax);
        return creatorDictionary;
    }
}
