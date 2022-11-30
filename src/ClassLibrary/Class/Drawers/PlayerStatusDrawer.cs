public class PlayerStatusDrawer
{
    private StringConverter stringConverter = new StringConverter();

    public void DrawPlayerStatus(int cursorLeft, int cursorTop, DataCenter dataCenter)
    {
        Console.WindowHeight = 200;
        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;
        List<string> strings = this.GenerateStrings(dataCenter);

        for (int i = 0; i < strings.Count(); i++)
        {
            this.stringConverter.WriteStringAtCenter(strings[i], 50);
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + i;
        }
    }

    public void DrawArrangedLines(int cursorLeft, int cursorTop, DataCenter dataCenter)
    {
        var stringLines = this.GenerateStringLines(dataCenter);
        List<int> spaces = new List<int> { 15, 9, 9, 9, 9 };
        this.stringConverter.WriteCenterArrangedLines(cursorLeft, cursorTop, stringLines, spaces);
    }

    private List<string> GenerateStrings(DataCenter dataCenter)
    {
        var strings = new List<string>();

        List<int> balancesInts = dataCenter.Bank.Balances;
        List<bool> areInGame = dataCenter.InGame.AreInGame;

        string balances = string.Empty;
        for (int i = 0; i < areInGame.Count(); i++)
        {
            if (areInGame[i] is true)
            {
                balances += balancesInts[i].ToString();
            }
            else
            {
                balances += "bankrupt";
            }

            if (i < areInGame.Count()-1)
            {
                balances += ", ";
            }
        }
        

        List<int> jailTurnCountsInts = dataCenter.Jail.TurnsInJailCounts;
        string jailTurnCounts = string.Join(", ", jailTurnCountsInts.ToArray());

        List<int> jailFreeCardCountsInts = dataCenter.Jail.JailFreeCardCounts;
        string jailFreeCardCounts = string.Join(", ", jailFreeCardCountsInts.ToArray());

        strings.Add("Balances : " + balances);
        strings.Add("Turns in Jail : " + jailTurnCounts);
///        strings.Add("FreeJailCards : " + jailTurnCounts);
        return strings;
    }

    private List<List<string>> GenerateStringLines(DataCenter dataCenter)
    {
        List<string> players = new List<string> { string.Empty, "Player0", "Player1", "Player2", "Player3" };

        List<int> balanceInts = dataCenter.Bank.Balances;
        List<string> balanceStrings = (from balance in balanceInts select balance.ToString()).ToList();
        balanceStrings.Insert(0, "Balances : ");

        List<int> jailTurnCountsInts = dataCenter.Jail.TurnsInJailCounts;
        List<string> jailTurnCounts = (from jailTurnCount in jailTurnCountsInts select jailTurnCount.ToString()).ToList();
        jailTurnCounts.Insert(0, "Turns in Jail : ");

///        List<int> jailFreeCardCountsInts = dataCenter.Jail.JailFreeCardCounts;
///        List<string> jailFreeCardCounts = (from jailFreeCard in jailFreeCardCountsInts select jailFreeCard.ToString()).ToList();
///        jailFreeCardCounts.Insert(0, "FreeJailCards : ");

        List<List<string>> stringLines = new List<List<string>> { players, balanceStrings, jailTurnCounts};

        return stringLines;
    }
}
