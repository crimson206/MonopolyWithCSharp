public class MapTilesFactory
{   

    public List<Tile> CreateRandomMapTiles(int numOfRealRestates, int numOfRailRoads, int numOfUtilities, int numOfChances, int numOfCommunityChests, Random random, int password)
    {
        ///RealEstate corner rail utily chance tax chest
        ///22 + 4 + 4 + 2 + 3+ 2 + 3
        List<Tile> mapTiles = new List<Tile>();
        List<Tile> realEstates = CreateRealEstateGroups(numOfRealRestates, 80, 400, random, password);
        List<RailRoad> railRoads = CreateRailRoads(numOfRailRoads, 200, 2, password);
        List<Utility> uitilities = CreateUtilities(numOfUtilities, 100, 4, 6, password);
        List<Tile> evenTiles = CreateEventTiles(numOfChances, numOfCommunityChests);
        List<Tile> taxs = new List<Tile> { new IncomeTax("Income Tax", 200, 10), new LuxuryTax("Luxury Tax", 200) };
        // 4 = corners 2 = tax
        int total = numOfRealRestates + numOfRailRoads + numOfUtilities + numOfChances + numOfCommunityChests + 4 + 2;

        for (int i = 0; i < total; i++)
        {


            int a = realEstates.Count();
            int b = railRoads.Count();
            int c = uitilities.Count();
            int d = evenTiles.Count();
            int e = taxs.Count();

            int rest = a + b + c + d +e;

            int rdnNum = random.Next(0, rest);

            if(i == 0)
            {
                mapTiles.Add( new Go("Go!", 200));
            }
            else if(i == 10)
            {
                mapTiles.Add( new Jail("Jail", 60));
            }
            else if(i == 20)
            {
                mapTiles.Add( new FreeParking("Free Parking"));
            }
            else if(i == 30)
            {
                mapTiles.Add( new GoToJail("Go To Jail"));
            }
            else if(rdnNum < a)
            {
                mapTiles.Add(realEstates[0]);
                realEstates.RemoveAt(0);
            }
            else if((rdnNum - a) < b)
            {
                mapTiles.Add(railRoads[0]);
                railRoads.RemoveAt(0);
            }
            else if((rdnNum - a - b) < c)
            {
                mapTiles.Add(uitilities[0]);
                uitilities.RemoveAt(0);
            }
            else if((rdnNum - a - b - c) < d)
            {
                mapTiles.Add(evenTiles[0]);
                evenTiles.RemoveAt(0);
            }
            else
            {
                mapTiles.Add(taxs[0]);
                taxs.RemoveAt(0);
            }
        }

        return mapTiles;

    }


    /// lastRentRate usually from 4. set min as 3
    /// set price limit 40
    private RealEstate CreateRealEstateWithAutoFinance(string name, int price, string color, int password)
    {

        /// price=40 => 1, price >> 50 => 2
        double priceAdvantage = (double) 2*price / (price + 60);
        /// buildingCost is about the half of the price if the price high
        int buildingCost = (int) (price / priceAdvantage);

        int basicRent = (int) (priceAdvantage * price / 20);
        int monopolyRent = 2 * basicRent;

        /// priceAdvantage for rents with houses
        double priceAdvantageForRentWithHouse = Math.Pow(priceAdvantage, 0.25);

        int house1Rent = ((int) (4 * priceAdvantageForRentWithHouse * basicRent)/5) * 5;
        int house2Rent = ((int) (3 * house1Rent)/5) * 5;
        int house3Rent = ((int) (7  * house1Rent)/10) * 10;
        int house4Rent = ((int) (8.5 * house1Rent)/10) * 10;
        int hotelRent  = ((int) (10 * house1Rent)/50) * 50;
        List<int> rents = new List<int> {basicRent,monopolyRent,house1Rent,house2Rent,house3Rent,house4Rent,hotelRent};

        int mortgageValue = price/2;

        return new RealEstate(name, price, rents, mortgageValue, color, password);
    }

    /// <summary>
    /// prices are difference, share the same rentrate
    /// </summary>
    /// <param name="groupSize"></param>
    /// <param name="color"></param>
    /// <param name="referencePrice"></param>
    /// <param name="lastRentRate"></param>
    /// <returns></returns>
    private List<Tile> CreateRealEstateColorGroup(int groupSize, string color, int refPrice, int password)
    {
        List<Tile> colorGroup = new List<Tile>();
        
        for (int i = 0; i < groupSize; i++)
        {
            double priceRate = 0.8 + 0.2 * i / (groupSize - 1);
            string name = String.Format("RealEstate {0}{1}", color, i+1);
            int price = (int) (priceRate * refPrice);
            Tile newRealEstate = CreateRealEstateWithAutoFinance(name, price, color, password);
            colorGroup.Add(newRealEstate);
        }
        return colorGroup;
    }

    private List<int> DistributeNumToThreeAndTwo(int number)
    {
        if(number%3 == 0)
        {
            return new List<int> {number/3,0};
        }
        else if(number%3 == 1)
        {
            return new List<int> {number/3-1,2};
        }
        else
        {
            return new List<int> {number/3,1};
        };
    }

    private List<Tile> CreateRealEstateGroups(int number, int startPrice, int endPrice, Random random, int password)
    {
        List<Tile> realEstates = new List<Tile>();
        
        List<int> numOfThreeAndTwo = DistributeNumToThreeAndTwo(number);
        List<int> threeTwoList = ListThreeTwoRandomly(numOfThreeAndTwo, random);
        int numberOfGroups = threeTwoList.Count();
        
        List<string> colors = new List<string> {"Black", "Blue", "Cyan", "DarkBlue", "DarkCyan", "DarkGray", "DarkGreen", "DarkMagenta", "DarkRed", "DarkYellow", "Gray", "Green"};

        int priceIncrease = (endPrice-startPrice)/(numberOfGroups-1);

        for (int i = 0; i < numberOfGroups; i++)
        {
            int groupSize = threeTwoList[i];
            int price = startPrice + (i * priceIncrease);
            List<Tile> newRealEstateGroup = CreateRealEstateColorGroup(threeTwoList[i], colors[i], price, password);
            
            for (int j = 0; j < groupSize; j++)
            {
                realEstates.Add(newRealEstateGroup[j]);
            }
        }

        return realEstates;
    }

    private List<int> ListThreeTwoRandomly(List<int> ThreeTwo, Random random)
    {
        List<int> threeTwoList = new List<int>();
        int listLength = ThreeTwo[0]+ThreeTwo[1];

        for (int i = 0; i < listLength; i++)
        {
            if(random.Next(0,listLength-i) < ThreeTwo[0])
            {
                ThreeTwo[0]--;
                threeTwoList.Add(3);
            }
            else
            {
                ThreeTwo[1]--;
                threeTwoList.Add(2);
            }
        }
        return threeTwoList;
    }

    private List<RailRoad> CreateRailRoads(int numOfRailRoads, int price, double rentIncreaseRate, int password)
    {
        List<RailRoad> railRoads = new List<RailRoad>();
        List<int> rents = new List<int>();
        int basicRent = price / 8;
        int mortgageValue = price/2;

        for (int i = 0; i < numOfRailRoads; i++)
        {
            int rent = (int) (basicRent * Math.Pow(rentIncreaseRate,i));
            rents.Add(rent);
        }

        for (int i = 0; i < numOfRailRoads; i++)
        {
            string name = String.Format("RailRoad{0}", i+1);

            RailRoad newRailRoad = new RailRoad(name, price, rents, mortgageValue, password);
            railRoads.Add(newRailRoad);
        }
        return railRoads;
    }

    private List<Utility> CreateUtilities(int numOfUtilities, int price, int basicRent, int addRent, int password)
    {
        List<Utility> utilities = new List<Utility>();
        List<int> rents = new List<int>();
        int mortgageValue = price/2;

        for (int i = 0; i < numOfUtilities; i++)
        {
            int rent = basicRent + (i * addRent);
            rents.Add(rent);
        }

        for (int i = 0; i < numOfUtilities; i++)
        {
            string name = String.Format("Utility{0}", i+1);

            Utility newUtility = new Utility(name, price, rents, mortgageValue, password);
            utilities.Add(newUtility);
        }
        return utilities;
    }

    private List<Tile> CreateEventTiles(int numOfChances, int numOfChests)
    {
        List<Tile> eventTiles = new List<Tile>();

        for (int i = 0; i < numOfChances; i++)
        {
            Chance newChance = new Chance("Chance");
            eventTiles.Add(newChance);
        }
        for (int i = 0; i < numOfChests; i++)
        {
            CommunityChest newCommunityChest = new CommunityChest("CommunityChest");
            eventTiles.Add(newCommunityChest);
        }
        return eventTiles;
    }

    private List<Tile> ShuffleTiles(List<Tile> tiles, Random random)
    {
        int total = tiles.Count();
        List<Tile> shuffled = new List<Tile>();
        for (int i = 0; i < total; i++)
        {
            Tile selected = tiles[random.Next(0,tiles.Count())];
            shuffled.Add(selected);
            tiles.Remove(selected);
            
        }

        return shuffled;
    }
}
