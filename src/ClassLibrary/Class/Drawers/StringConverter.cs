public class StringConverter
{
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

    public string ConvertListIntToString(List<int> ints)
    {
        string str = "[";
        foreach (var item in ints)
        {
            str += item.ToString() +", ";
        }
        str += "]";
        return str;
    }

    public void WriteStringWithColorAtCenter(string str, string color, int length)
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
    public void WriteStringAtCenter(string str, int length)
    {
        int emptyLength = length - str.Count();
        Console.Write(EmptyStr(emptyLength/2));
        Console.Write(str);
        Console.Write(EmptyStr((emptyLength+1)/2));
    }

    public string ArrangeStringList(List<string> strings, List<int> spaces)
    {
        if ( strings.Count() != spaces.Count())
        {
            throw new Exception();
        }

        string newLine = String.Empty;
        for (int i = 0; i < strings.Count(); i++)
        {
            newLine += ArrangeCenter(strings[i], spaces[i]);
        }
                    
        return newLine;

    }

    public void WriteCenterArrangedLines(int cursorLeft, int cursorTop, List<List<string>> stringLines, List<int> spaceOfEachStringElementOfList)
    {
        if (stringLines.Any(stringLine => stringLine.Count() != spaceOfEachStringElementOfList.Count()))
        {
            throw new Exception();
        }

        for (int i = 0; i < stringLines.Count(); i++)
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + i;
            string arrangedLine = this.ArrangeStringList(stringLines[i], spaceOfEachStringElementOfList);
            Console.Write(arrangedLine);
        }

    }

}