public class DisplayTileInfo
{
    public void DisplayRealEstates(int cursorLeft, int cursorTop, List<IRealEstateData> realEstates, int buffer)
    {
        int maxNameLength = realEstates.Max(t => t.Name.Count());
        int maxPriceLength = realEstates.Max(t => t.Price.ToString().Count());

        int spaceForName = maxNameLength + buffer;
        int spaceForPrice = maxPriceLength + buffer;
        int spaceForRents = 4 + buffer;
        int spaceForHouses = 6 + buffer;
        int spaceForOwner = 7 + buffer;
        int spaceForMortgaged = 9 + buffer;

        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;

        Console.Write(this.CreateRealEstatesBanner(spaceForName, spaceForPrice, spaceForRents, spaceForHouses, spaceForOwner, spaceForMortgaged));

        int index = 1;
        foreach (var realEstate in realEstates)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + index;

            this.WriteRealEstateWithColor(realEstate, spaceForName, spaceForPrice, spaceForRents, spaceForHouses, spaceForOwner, spaceForMortgaged);

            index++;
        }
    }

    public void DisplayRailRoad(int cursorLeft, int cursorTop, List<IRailRoadData> railRoads, int buffer)
    {
        int maxNameLength = railRoads.Max(t => t.Name.Count());
        int maxPriceLength = railRoads.Max(t => t.Price.ToString().Count());

        int spaceForName = maxNameLength + buffer;
        int spaceForPrice = maxPriceLength + buffer;
        int spaceForRents = 4 + buffer;
        int spaceForOwner = 7 + buffer;
        int spaceForMortgaged = 9 + buffer;

        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;

        this.WriteRailRoadBanner(spaceForName, spaceForPrice, spaceForRents, spaceForOwner, spaceForMortgaged);

        int index = 1;

        foreach (var railRoad in railRoads)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + index;

            this.WriteRailRoad(railRoad, spaceForName, spaceForPrice, spaceForRents, spaceForOwner, spaceForMortgaged);

            index++;
        }
    }

    public void DisplayUtility(int cursorLeft, int cursorTop, List<IUtilityData> utilityDatas, int buffer)
    {
        int maxNameLength = utilityDatas.Max(t => t.Name.Count());
        int maxPriceLength = utilityDatas.Max(t => t.Price.ToString().Count());

        int spaceForName = maxNameLength + buffer;
        int spaceForPrice = maxPriceLength + buffer;
        int spaceForRents = 4 + buffer;
        int spaceForOwner = 7 + buffer;
        int spaceForMortgaged = 9 + buffer;

        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;

        this.WriteUtilityBanner(spaceForName, spaceForPrice, spaceForRents, spaceForOwner, spaceForMortgaged);

        int index = 1;

        foreach (var utilityData in utilityDatas)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + index;

            this.WriteUtility(utilityData, spaceForName, spaceForPrice, spaceForRents, spaceForOwner, spaceForMortgaged);

            index++;
        }
    }

    private void WriteRailRoadBanner(int spaceForName, int spaceForPrice, int spaceForRent, int spaceForOwner, int spaceForMortgaged)
    {
        this.WriteStringAtCenter("RailRoads", spaceForName);
        this.WriteStringAtCenter("Price", spaceForPrice);
        this.WriteStringAtCenter("Rent", spaceForRent);
        this.WriteStringAtCenter("Owner", spaceForOwner);
        this.WriteStringAtCenter("Mortgaged", spaceForMortgaged);
    }

    private void WriteUtilityBanner(int spaceForName, int spaceForPrice, int spaceForRent, int spaceForOwner, int spaceForMortgaged)
    {
        this.WriteStringAtCenter("Utilities", spaceForName);
        this.WriteStringAtCenter("Price", spaceForPrice);
        this.WriteStringAtCenter("Rent", spaceForRent);
        this.WriteStringAtCenter("Owner", spaceForOwner);
        this.WriteStringAtCenter("Mortgaged", spaceForMortgaged);
    }

    private string CreateRealEstatesBanner(int nameL, int priceL, int rentL, int numHouseL, int ownerL, int mortgagedL)
    {
        string realestateBanner = this.ArrangeCenter("RealEstates", nameL) +
                                this.ArrangeCenter("Price", priceL) + this.ArrangeCenter("Rent", rentL) +
                                this.ArrangeCenter("Houses", numHouseL) + this.ArrangeCenter("Owner", ownerL) +
                                this.ArrangeCenter("Mortgaged", mortgagedL);;
        return realestateBanner;
    }

    private void WriteRailRoad(IRailRoadData railRoadData, int nameL, int priceL, int rentL, int ownerL, int mortgagedL)
    {
        string ownerIDToStr = "Player" + railRoadData.OwnerPlayerNumber.ToString();
        string stringRents = railRoadData.CurrentRent.ToString();

        this.WriteStringAtCenter(railRoadData.Name, nameL);
        this.WriteStringAtCenter(railRoadData.Price.ToString(), priceL);
        this.WriteStringAtCenter(stringRents, rentL);
        if (railRoadData.OwnerPlayerNumber is null)
        {
            this.WriteStringAtCenter("free", ownerL);
        }
        else
        {
            this.WriteStringAtCenter(ownerIDToStr, ownerL);
        }
        this.WriteStringAtCenter(railRoadData.IsMortgaged.ToString(), mortgagedL);
    }

    private void WriteUtility(IUtilityData utilityData, int nameL, int priceL, int rentL, int ownerL, int mortgagedL)
    {
        string ownerIDToStr = "Player" + utilityData.OwnerPlayerNumber.ToString();
        string stringRents = utilityData.CurrentRent.ToString();

        this.WriteStringAtCenter(utilityData.Name, nameL);
        this.WriteStringAtCenter(utilityData.Price.ToString(), priceL);
        this.WriteStringAtCenter(stringRents + "x", rentL);
        if (utilityData.OwnerPlayerNumber is null)
        {
            this.WriteStringAtCenter("free", ownerL);
        }
        else
        {
            this.WriteStringAtCenter(ownerIDToStr, ownerL);
        }
        this.WriteStringAtCenter(utilityData.IsMortgaged.ToString(), mortgagedL);
    }

    private void WriteRealEstateWithColor(IRealEstateData realEstateData, int nameL, int priceL, int rentL, int numHouseL, int ownerL, int mortgagedL)
    {
        string ownerIDToStr = "Player" + realEstateData.OwnerPlayerNumber.ToString();
        string stringRents = realEstateData.CurrentRent.ToString();

        this.WriteStringWithColorAtCenter(realEstateData.Name, realEstateData.Color, nameL);
        this.WriteStringAtCenter(realEstateData.Price.ToString(), priceL);
        this.WriteStringAtCenter(stringRents, rentL);
        this.WriteStringAtCenter(realEstateData.HouseCount.ToString(), numHouseL);

        if (realEstateData.OwnerPlayerNumber is null)
        {
            this.WriteStringAtCenter("free", ownerL);
        }
        else
        {
            this.WriteStringAtCenter(ownerIDToStr, ownerL);
        }
        this.WriteStringAtCenter(realEstateData.IsMortgaged.ToString(), mortgagedL);
    }

    private string ConvertRealEstateToStr(IRealEstateData realEstate, int nameL, int colorL, int priceL, int rentL, int numHouseL, int ownerL)
    {
        string ownerIDToStr = "Player" + realEstate.OwnerPlayerNumber.ToString();
        string stringRents = this.ConvertListIntToString(realEstate.Rents);

        string realestateBanner = this.ArrangeCenter(realEstate.Name, nameL) + this.ArrangeCenter(realEstate.Color, colorL) +
                                this.ArrangeCenter(realEstate.Price.ToString(), priceL) + this.ArrangeCenter(stringRents, rentL) +
                                this.ArrangeCenter(realEstate.HouseCount.ToString(), numHouseL) + this.ArrangeCenter(ownerIDToStr, ownerL);
        return realestateBanner;
    }

    private string EmptyStr(int length)
    {
        string emptyString = string.Empty;
        for (int i = 0; i < length; i++)
        {
            emptyString += " ";
        }

        return emptyString;
    }

    private string ArrangeCenter(string str, int length)
    {
        int emptyLength = length - str.Count();
        string frontEmpty = this.EmptyStr(emptyLength / 2);
        string backEmpty = this.EmptyStr((emptyLength + 1) / 2);
        return frontEmpty + str + backEmpty;
    }

    private string ConvertListIntToString(List<int> ints)
    {
        string str = "[";
        foreach (var item in ints)
        {
            str += item.ToString() + ", ";
        }

        str += "]";
        return str;
    }

    private void WriteStringWithColorAtCenter(string str, string color, int length)
    {
        ConsoleColor backUpColor = Console.ForegroundColor;
        ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        int emptyLength = length - str.Count();
        foreach (var consoleColor in colors)
        {
            if (color == consoleColor.ToString())
            {
                Console.ForegroundColor = consoleColor;
                Console.Write(this.EmptyStr(emptyLength / 2));
                Console.Write(str);
                Console.Write(this.EmptyStr((emptyLength + 1) / 2));
            }
        }

        Console.ForegroundColor = backUpColor;
    }

    private void WriteStringAtCenter(string str, int length)
    {
        int emptyLength = length - str.Count();
        Console.Write(this.EmptyStr(emptyLength / 2));
        Console.Write(str);
        Console.Write(this.EmptyStr((emptyLength + 1) / 2));
    }
}
