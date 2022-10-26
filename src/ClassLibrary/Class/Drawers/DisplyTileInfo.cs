using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class DisplayTileInfo
{
    private string CreateRealEstatesBanner (int nameL, int colorL, int priceL, int rentL, int numHouseL, int ownerL)
    {
        string realestateBanner = ArrangeCenter("RealEstates", nameL) + ArrangeCenter("Color", colorL) + 
                                ArrangeCenter("Price", priceL) + ArrangeCenter("Rent", rentL) + 
                                ArrangeCenter("Houses", numHouseL) + ArrangeCenter("Owner", ownerL);
        return realestateBanner;
    }

    private string CreateRealEstatesBanner2 (int nameL, int priceL, int rentL, int numHouseL, int ownerL)
    {
        string realestateBanner = ArrangeCenter("RealEstates", nameL) + 
                                ArrangeCenter("Price", priceL) + ArrangeCenter("Rent", rentL) + 
                                ArrangeCenter("Houses", numHouseL) + ArrangeCenter("Owner", ownerL);
        return realestateBanner;
    }


    public void DisplayRealEstates (int cursorLeft, int cursorTop, List<RealEstateData> realEstates , int buffer)
    {
        int maxNameLength = realEstates.Max(t => t.Name.Count());
        int maxPriceLength = realEstates.Max(t => t.Price.ToString().Count());
        
        int spaceForName = maxNameLength + buffer;
        int spaceForPrice = maxPriceLength + buffer;
        int spaceForRents = 4 + buffer;
        int spaceForHouses = 6 + buffer;
        int spaceForOwner = 7 + buffer;

        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;

        Console.Write(CreateRealEstatesBanner2(spaceForName, spaceForPrice, spaceForRents, spaceForHouses, spaceForOwner));

        int index = 1;
        foreach (var realEstate in realEstates)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + index;

            WriteRealEstateWithColor(realEstate, spaceForName,
            spaceForPrice, spaceForRents, spaceForHouses, spaceForOwner);

            index++;
        }
    }

    public void DisplayRailRoad(int cursorLeft, int cursorTop, List<RailRoadData> railRoads , int buffer)
    {
        int maxNameLength = railRoads.Max(t => t.Name.Count());
        int maxPriceLength = railRoads.Max(t => t.Price.ToString().Count());
        
        int spaceForName = maxNameLength + buffer;
        int spaceForPrice = maxPriceLength + buffer;
        int spaceForRents = 4 + buffer;
        int spaceForOwner = 7 + buffer;

        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;

        WriteRailRoadBanner(spaceForName, spaceForPrice, spaceForRents, spaceForOwner);

        int index = 1;
        foreach (var railRoad in railRoads)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + index;

            WriteRailRoad(railRoad, spaceForName, spaceForPrice,
            spaceForRents, spaceForOwner);

            index++;
        }
    }

    public void DisplayUtility(int cursorLeft, int cursorTop, List<UtilityData> utilityDatas , int buffer)
    {
        int maxNameLength = utilityDatas.Max(t => t.Name.Count());
        int maxPriceLength = utilityDatas.Max(t => t.Price.ToString().Count());
        
        int spaceForName = maxNameLength + buffer;
        int spaceForPrice = maxPriceLength + buffer;
        int spaceForRents = 4 + buffer;
        int spaceForOwner = 7 + buffer;

        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;

        WriteUtilityBanner(spaceForName, spaceForPrice, spaceForRents, spaceForOwner);

        int index = 1;
        foreach (var utilityData in utilityDatas)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + index;

            WriteUtility(utilityData, spaceForName, spaceForPrice,
            spaceForRents, spaceForOwner);

            index++;
        }
    }





    private void WriteRailRoadBanner(int spaceForName, int  spaceForPrice, int  spaceForRent, int spaceForOwner)
    {
        WriteStringAtCenter("RailRoads", spaceForName);
        WriteStringAtCenter("Price", spaceForPrice);
        WriteStringAtCenter("Rent", spaceForRent);
        WriteStringAtCenter("Owner", spaceForOwner); 
    }

    private void WriteUtilityBanner(int spaceForName, int  spaceForPrice, int  spaceForRent, int spaceForOwner)
    {
        WriteStringAtCenter("Utilities", spaceForName);
        WriteStringAtCenter("Price", spaceForPrice);
        WriteStringAtCenter("Rent", spaceForRent);
        WriteStringAtCenter("Owner", spaceForOwner); 
    }


    private void WriteRailRoad(RailRoadData railRoadData, int nameL, int priceL, int rentL, int ownerL)
    {
        string ownerIDToStr = "Player" + railRoadData.OwnerPlayerNumber.ToString();
        string stringRents = railRoadData.Rents[0].ToString();

        WriteStringAtCenter(railRoadData.Name, nameL);
        WriteStringAtCenter(railRoadData.Price.ToString(), priceL);
        WriteStringAtCenter(stringRents, rentL);
        if (railRoadData.OwnerPlayerNumber is null)
        {
            WriteStringAtCenter("free", ownerL);  
        }
        else
        {
            WriteStringAtCenter(ownerIDToStr, ownerL);   
        }                        
    }

    private void WriteUtility(UtilityData utilityData, int nameL, int priceL, int rentL, int ownerL)
    {
        string ownerIDToStr = "Player" + utilityData.OwnerPlayerNumber.ToString();
        string stringRents = utilityData.Rents[0].ToString();

        WriteStringAtCenter(utilityData.Name, nameL);
        WriteStringAtCenter(utilityData.Price.ToString(), priceL);
        WriteStringAtCenter(stringRents+"x", rentL);
        if (utilityData.OwnerPlayerNumber is null)
        {
            WriteStringAtCenter("free", ownerL);  
        }
        else
        {
            WriteStringAtCenter(ownerIDToStr, ownerL);   
        }                        
    }



    private void WriteRealEstateWithColor (RealEstateData realEstate, int nameL, int priceL, int rentL, int numHouseL, int ownerL)
    {
        string ownerIDToStr = "Player" + realEstate.OwnerPlayerNumber.ToString();
        string stringRents = realEstate.Rents[0].ToString();

        WriteStringWithColorAtCenter(realEstate.Name, realEstate.Color, nameL);
        WriteStringAtCenter(realEstate.Price.ToString(), priceL);
        WriteStringAtCenter(stringRents, rentL);
        WriteStringAtCenter(realEstate.HouseCount.ToString(), numHouseL);
        if (realEstate.OwnerPlayerNumber is null)
        {
            WriteStringAtCenter("free", ownerL);  
        }
        else
        {
            WriteStringAtCenter(ownerIDToStr, ownerL);   
        }                        
    }

    private string ConvertRealEstateToStr (RealEstateData realEstate, int nameL, int colorL, int priceL, int rentL, int numHouseL, int ownerL)
    {
        string ownerIDToStr = "Player" + realEstate.OwnerPlayerNumber.ToString();
        string stringRents = ConvertListIntToString(realEstate.Rents);

        string realestateBanner = ArrangeCenter(realEstate.Name, nameL) + ArrangeCenter(realEstate.Color, colorL) + 
                                ArrangeCenter(realEstate.Price.ToString(), priceL) + ArrangeCenter(stringRents, rentL) + 
                                ArrangeCenter(realEstate.HouseCount .ToString(), numHouseL) + ArrangeCenter(ownerIDToStr, ownerL);                                
        return realestateBanner;
    }

    private string EmptyStr (int length)
    {
        string emptyString = String.Empty;
        for (int i = 0; i < length; i++)
        {
            emptyString += " ";
        }
        return emptyString;
    }

    private string ArrangeCenter(string str, int length)
    {
        int emptyLength = length - str.Count();
        string frontEmpty = EmptyStr(emptyLength / 2);
        string backEmpty = EmptyStr((emptyLength+1) / 2);
        return frontEmpty + str + backEmpty;
    }

    private string ConvertListIntToString(List<int> ints)
    {
        string str = "[";
        foreach (var item in ints)
        {
            str += item.ToString() +", ";
        }
        str += "]";
        return str;
    }

    private void WriteStringWithColorAtCenter(string str, string color, int length)
    {
        ConsoleColor backUpColor = Console.ForegroundColor;
        ConsoleColor[] colors = (ConsoleColor[]) Enum.GetValues(typeof(ConsoleColor));
        int emptyLength = length - str.Count();
        foreach (var consoleColor in colors)
        {
            if (color == consoleColor.ToString())
            {
                Console.ForegroundColor = consoleColor;
                Console.Write(EmptyStr(emptyLength/2));
                Console.Write(str);
                Console.Write(EmptyStr((emptyLength+1)/2));
            }
        }
        Console.ForegroundColor = backUpColor;
    }
    private void WriteStringAtCenter(string str, int length)
    {
        int emptyLength = length - str.Count();
        Console.Write(EmptyStr(emptyLength/2));
        Console.Write(str);
        Console.Write(EmptyStr((emptyLength+1)/2));
    }
}