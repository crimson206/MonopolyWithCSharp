public class PlayerStatusDrawer
{
    private StringConverter stringConverter = new StringConverter();

    public void DrawPlayerStatusWithArrangedLines(int cursorLeft, int cursorTop, DataCenter dataCenter)
    {
        var stringLines = this.GenerateStringLines(dataCenter);
        List<int> spaces = new List<int> { 15, 9, 9, 9, 9 };
        this.stringConverter.WriteCenterArrangedLines(cursorLeft, cursorTop, stringLines, spaces);
    }

    private List<List<string>> GenerateStringLines(DataCenter dataCenter)
    {
        List<string> players = new List<string> { string.Empty, "Player0", "Player1", "Player2", "Player3" };

        List<int> balanceInts = dataCenter.Bank.Balances;
        List<string> balanceStrings = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            if (dataCenter.InGame.AreInGame[i])
            {
                balanceStrings.Add(balanceInts[i].ToString());
            }
            else
            {
                balanceStrings.Add("bankrupt");
            }
        }
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
