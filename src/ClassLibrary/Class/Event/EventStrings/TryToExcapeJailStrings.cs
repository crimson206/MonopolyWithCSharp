public class TryToExcapeJailStrings
{
    public string Start(int playerNumber)
    {
        return String.Format("Player{0} is in jail", playerNumber);
    }
    public string CanPlayerUseJailFreeCard(int playerNumber, int amountFreeJailCard)
    {
        return String.Format("Player{0} has {1} jailfree card(s). Does Player{0} want to use a jailfree card?", playerNumber, amountFreeJailCard);
    }

    public string CanPlayerPayJailFine(int playerNumber)
    {
        return String.Format("Does Player{0} want to pay the jail fine to escape immediately?", playerNumber);
    }

    public string RollPlayerDiceToEscape(int playerNumber, int[] rollDiceResult)
    {
        return String.Format("Player{0} rolled dice to try to escape and the result is {1}", playerNumber, String.Join(", ", rollDiceResult));
    }

    public string StaypedPlayerThreeTurnsInJailTrue(int playerNumber)
    {
        return String.Format("Player{0} stayed 3 turns in the jail and paid the jail fine", playerNumber);
    }

    public string StaypedPlayerThreeTurnsInJailFalse(int playerNumber)
    {
        return String.Format("Player{0} couldn't escape the jail", playerNumber);
    }

}
