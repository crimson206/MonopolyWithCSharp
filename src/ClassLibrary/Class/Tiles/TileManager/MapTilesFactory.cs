public class MapTilesFactory
{
    private Random random = new Random();
    private GroupSetter groupSetter = new GroupSetter();

    public List<ITile> CreateRandomMapTiles(
        int numOfRealRestates,
        int numOfRailRoads, 
        int numOfUtilities,
        int numOfChances, 
        int numOfCommunityChests,
        bool twoTaxes,
        int goPosition,
        int jailPosition,
        int freeParkingPosition,
        int goToJailPositoin
        )
    {
        List<List<ITile>> differentTileGroups = this.CreateDifferentTileGroups(numOfRealRestates, numOfRailRoads, numOfUtilities, numOfChances, numOfCommunityChests, twoTaxes);

        List<ITile> randomlyAssembledTiles = differentTileGroups[0];

        for (int i = 1; i < differentTileGroups.Count(); i++)
        {
            randomlyAssembledTiles = this.AssembleTwoTileGroupsRandomly(differentTileGroups[i], randomlyAssembledTiles, this.random);
        }

        int totalSize = numOfRealRestates + numOfRailRoads + numOfUtilities + numOfChances + numOfCommunityChests + 6;

        randomlyAssembledTiles.Insert(goPosition, new Go("Go!", 200));
        randomlyAssembledTiles.Insert(jailPosition, new Jail("Jail", 60));
        randomlyAssembledTiles.Insert(freeParkingPosition, new FreeParking("FreeParking"));
        randomlyAssembledTiles.Insert(goToJailPositoin, new GoToJail("GoToJail"));

        this.groupSetter.SetGroups(randomlyAssembledTiles);

        return randomlyAssembledTiles;
    }

    public List<ITileData> ExtractTileDataSet(List<ITile> tiles)
    {
        List<ITileData> tileDataSet = new List<ITileData>();

        foreach (var tile in tiles)
        {
            if (tile is Go)
            {
                tileDataSet.Add(tile);
            }
            else if (tile is Jail)
            {
                tileDataSet.Add(tile);
            }
            else if (tile is RealEstate)
            {
                var realEstate = (RealEstate)tile;
                tileDataSet.Add(realEstate);
            }
            else if (tile is Utility)
            {
                var utility = (Utility)tile;
                tileDataSet.Add(utility);
            }
            else if (tile is RailRoad)
            {
                var railRoad = (RailRoad)tile;
                tileDataSet.Add(railRoad);
            }
            else if (tile is IncomeTax)
            {
                tileDataSet.Add(tile);
            }
            else if (tile is LuxuryTax)
            {
                tileDataSet.Add(tile);
            }
            else
            {
                tileDataSet.Add(tile);
            }
        }

        return tileDataSet;
    }

    private List<List<ITile>> CreateDifferentTileGroups(int numOfRealRestates, int numOfRailRoads, int numOfUtilities, int numOfChances, int numOfCommunityChests, bool twoTaxes)
    {

        List<List<ITile>> differentTileGroups = new List<List<ITile>>();
        differentTileGroups.Add(this.CreateRealEstateGroups(numOfRealRestates, 80, 400, this.random));
        differentTileGroups.Add(this.CreateRailRoads(numOfRailRoads, 200, (6 - numOfRailRoads)));
        differentTileGroups.Add(this.CreateUtilities(numOfUtilities, 100, 4, 6));
        differentTileGroups.Add(this.CreateEventTiles(numOfChances, numOfCommunityChests));
        if (twoTaxes)
        {
            differentTileGroups.Add(this.CreateTaxes());
        }

        return differentTileGroups;
    }

    private List<int> ExtractRandomIndeces(int randomIndecesSize, int totalSize, Random random)
    {
        List<int> range = Enumerable.Range(0, totalSize).ToList();
        List<int> randomIndeces = new List<int>();

        for (int i = 0; i < randomIndecesSize; i++)
        {
            int rndNum = random.Next(0, totalSize - i);
            randomIndeces.Add(range[rndNum]);
            range.RemoveAt(rndNum);
        }

        randomIndeces.Sort();
        return randomIndeces;
    }

    private List<ITile> AssembleTwoTileGroupsRandomly(List<ITile> tiles1, List<ITile> tiles2, Random random)
    {
        int tiles1Size = tiles1.Count();
        int totalSize = tiles1Size + tiles2.Count();
        List<ITile> assembledTiles = tiles2;
        List<int> randomIndeces = this.ExtractRandomIndeces(tiles1Size, totalSize, random);

        for (int i = 0; i < tiles1Size; i++)
        {
            assembledTiles.Insert(randomIndeces[i], tiles1[i]);
        }

        return assembledTiles;
    }

    private RealEstate CreateRealEstateWithAutoFinance(string name, int price, string color)
    {
        /// price = 60 => 1, price >> 60 => 2
        double priceAdvantage = (double)2 * price / (price + 60);

        /// buildingCost is about the half of the price if the price high
        int buildingCost = (int)(price / priceAdvantage);

        int basicRent = (int)(priceAdvantage * price / 20);
        int monopolyRent = 2 * basicRent;

        /// priceAdvantage for rents with houses
        double priceAdvantageForRentWithHouse = Math.Pow(priceAdvantage, 0.25);

        int house1Rent = ((int)(4 * priceAdvantageForRentWithHouse * basicRent) / 5) * 5;
        int house2Rent = ((int)(3 * house1Rent) / 5) * 5;
        int house3Rent = ((int)(7  * house1Rent) / 10) * 10;
        int house4Rent = ((int)(8.5 * house1Rent) / 10) * 10;
        int hotelRent  = ((int)(10 * house1Rent) / 50) * 50;

        List<int> rents = new List<int> { basicRent , monopolyRent , house1Rent , house2Rent , house3Rent , house4Rent , hotelRent };

        int mortgageValue = price / 2;

        return new RealEstate(name, price, buildingCost, rents, mortgageValue, color);
    }

    private List<int> DistributeNumToThreeAndTwo(int number)
    {
        if (number % 3 == 0)
        {
            return new List<int> { number / 3 , 0 };
        }
        else if (number % 3 == 1)
        {
            return new List<int> { number / 3 - 1,2};
        }
        else
        {
            return new List<int> { number / 3 , 1 };
        };
    }

    private List<ITile> CreateRealEstateGroups(int number, int startPrice, int endPrice, Random random)
    {
        List<ITile> realEstates = new List<ITile>();

        List<int> numOfThreeAndTwo = this.DistributeNumToThreeAndTwo(number);
        List<int> threeTwoList = this.ListThreeTwoRandomly(numOfThreeAndTwo, random);
        int numberOfGroups = threeTwoList.Count();

        List<string> colors = new List<string> { "Black", "Blue", "Cyan", "DarkBlue", "DarkCyan", "DarkGray", "DarkGreen", "DarkMagenta", "DarkRed", "DarkYellow", "Gray", "Green" };

        int priceIncrease = (endPrice-startPrice) / (numberOfGroups - 1);

        for (int i = 0; i < numberOfGroups; i++)
        {
            int groupSize = threeTwoList[i];
            int price = startPrice + (i * priceIncrease);
            List<ITile> newRealEstateGroup = this.CreateRealEstateColorGroup(threeTwoList[i], colors[i], price);

            for (int j = 0; j < groupSize; j++)
            {
                realEstates.Add(newRealEstateGroup[j]);
            }
        }

        return realEstates;
    }

    private List<int> ListThreeTwoRandomly(List<int> threeTwo, Random random)
    {
        List<int> threeTwoList = new List<int>();
        int listLength = threeTwo[0] + threeTwo[1];

        for (int i = 0; i < listLength; i++)
        {
            if (random.Next(0 , listLength - i) < threeTwo[0])
            {
                threeTwo[0]--;
                threeTwoList.Add(3);
            }
            else
            {
                threeTwo[1]--;
                threeTwoList.Add(2);
            }
        }

        return threeTwoList;
    }

    private List<ITile> CreateRealEstateColorGroup(int groupSize, string color, int refPrice)
    {
        List<ITile> colorGroup = new List<ITile>();

        for (int i = 0; i < groupSize; i++)
        {
            double priceRate = 0.8 + 0.2 * i / (groupSize - 1);
            string name = String.Format("{0}{1}", color, i + 1);
            int price = (int)(priceRate * refPrice);
            ITile newRealEstate = this.CreateRealEstateWithAutoFinance(name, price, color);
            colorGroup.Add(newRealEstate);
        }

        return colorGroup;
    }

    private List<ITile> CreateRailRoads(int numOfRailRoads, int price, double rentIncreaseRate)
    {
        List<ITile> railRoadGroup = new List<ITile>();
        List<int> rents = new List<int>();
        int basicRent = price / 8;
        int mortgageValue = price / 2;

        for (int i = 0; i < numOfRailRoads; i++)
        {
            int rent = (int)(basicRent * Math.Pow(rentIncreaseRate, i));
            rents.Add(rent);
        }

        for (int i = 0; i < numOfRailRoads; i++)
        {
            string name = String.Format("RailRoad{0}", i + 1);

            ITile newRailRoad = new RailRoad(name, price, rents, mortgageValue);
            railRoadGroup.Add(newRailRoad);
        }

        return railRoadGroup;
    }

    private List<ITile> CreateUtilities(int numOfUtilities, int price, int basicRent, int addRent)
    {
        List<ITile> utilityGroup = new List<ITile>();
        List<int> rents = new List<int>();
        int mortgageValue = price / 2;

        for (int i = 0; i < numOfUtilities; i++)
        {
            int rent = basicRent + (i * addRent);
            rents.Add(rent);
        }

        for (int i = 0; i < numOfUtilities; i++)
        {
            string name = String.Format("Utility{0}", i+1);

            ITile newUtility = new Utility(name, price, rents, mortgageValue);
            utilityGroup.Add(newUtility);
        }

        return utilityGroup;
    }

    private List<ITile> CreateEventTiles(int numOfChances, int numOfChests)
    {
        List<ITile> eventTiles = new List<ITile>();

        for (int i = 0; i < numOfChances; i++)
        {
            Chance newChance = new Chance("Chance");
            eventTiles.Add(newChance);
        }

        for (int i = 0; i < numOfChests; i++)
        {
            CommunityChest newCommunityChest = new CommunityChest("Community Chest");
            eventTiles.Add(newCommunityChest);
        }

        return eventTiles;
    }

    private List<ITile> CreateTaxes()
    {
        return new List<ITile> { new IncomeTax("Income Tax", 200, 10), new LuxuryTax("Luxury Tax", 200) };
    }

    private List<ITile> ShuffleTiles(List<ITile> tiles, Random random)
    {
        int total = tiles.Count();
        List<ITile> shuffled = new List<ITile>();
        for (int i = 0; i < total; i++)
        {
            ITile selected = tiles[random.Next(0,tiles.Count())];
            shuffled.Add(selected);
            tiles.Remove(selected);
        }

        return shuffled;
    }
}
