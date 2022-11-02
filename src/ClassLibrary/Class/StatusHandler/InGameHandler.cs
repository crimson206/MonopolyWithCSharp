public class InGameHandler
{
    private List<bool> areInGame = new List<bool>();

    public InGameHandler(int joinedPlayerNumber)
    {
        for (int i = 0; i < joinedPlayerNumber; i++)
        {
            areInGame.Add(true);
        }
    }

    public List<bool> AreInGame => new List<bool>(this.areInGame);

    public void SetIsInGame(int playerNumber, bool isInGame)
    {
        this.areInGame[playerNumber] = isInGame;
    }
}