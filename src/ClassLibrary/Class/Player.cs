
public class Player
{
    private static int indexer = 0;
    private int playerNumber;


    public Player()
    {
        this.playerNumber = indexer;
        indexer++;
    }
    public int PlayerNumber { get => this.playerNumber; }
    public bool WantToUseJailFreeCard(JailManager jailManager, Bank bank, TileManager tilesManager)
    {
        if (jailManager.FreeJailCards[this.playerNumber] != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
