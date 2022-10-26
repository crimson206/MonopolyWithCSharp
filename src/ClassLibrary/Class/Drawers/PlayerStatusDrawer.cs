
public class PlayerStatusDrawer
{
    
    private StringConverter stringConverter = new StringConverter();

    public void DrawPlayerStatus(int cursorLeft, int cursorTop, DataCenter dataCenter)
    {
        Console.WindowHeight = 200;
        Console.CursorLeft = cursorLeft;
        Console.CursorTop = cursorTop;
        List<string> strings = GenerateStrings(dataCenter);

        for (int i = 0; i < strings.Count(); i++)
        {
            this.stringConverter.WriteStringAtCenter(strings[i], 50);
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop + i;
        
        }
    }

    private List<string> GenerateStrings(DataCenter dataCenter)
    {
        var strings = new List<string> ();

        List<int> balancesInts = dataCenter.Bank.Balances;
        string balances = String.Join(", ", balancesInts.ToArray());

        List<int> jailTurnCountsInts = dataCenter.Jail.TurnsInJailCounts;
        string jailTurnCounts = String.Join(", ", jailTurnCountsInts.ToArray());

        List<int> jailFreeCardCountsInts = dataCenter.Jail.FreeJailCardCounts;
        string jailFreeCardCounts = String.Join(", ", jailFreeCardCountsInts.ToArray());

        strings.Add("Balances : " + balances);
        strings.Add("Turns in Jail : " + jailTurnCounts);
        strings.Add("FreeJailCards : " + jailTurnCounts);
        return strings;
    }

    private List<List<string>> GenerateStringLines(DataCenter dataCenter)
    {
        List<string> players = new List<string> { "", "Player0", "Player1", "Player2", "Player3" };

        List<int> balanceInts = dataCenter.Bank.Balances;
        List<string> balanceStrings = (from balance in balanceInts select balance.ToString()).ToList();
        balanceStrings.Insert(0, "Balances : ");

        List<int> jailTurnCountsInts = dataCenter.Jail.TurnsInJailCounts;
        List<string> jailTurnCounts = (from jailTurnCount in jailTurnCountsInts select jailTurnCount.ToString()).ToList();
        jailTurnCounts.Insert(0, "Turns in Jail : ");

        List<int> jailFreeCardCountsInts = dataCenter.Jail.FreeJailCardCounts;
        List<string> jailFreeCardCounts = (from jailFreeCard in jailFreeCardCountsInts select jailFreeCard.ToString()).ToList();
        jailFreeCardCounts.Insert(0, "FreeJailCards : ");

        List<List<string>> stringLines = new List<List<string>> { players, balanceStrings, jailTurnCounts, jailFreeCardCounts};

        return stringLines;
    }

    
    public void DrawArrangedLines(int cursorLeft, int cursorTop, DataCenter dataCenter)
    {

        var stringLines = this.GenerateStringLines(dataCenter);

        List<int> spaces = new List<int> { 15, 9, 9, 9, 9};

        this.stringConverter.WriteCenterArrangedLines(cursorLeft, cursorTop, stringLines, spaces);
    }

}