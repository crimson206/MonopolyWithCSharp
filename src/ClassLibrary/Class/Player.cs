
public class Player
{
    private static int indexer = 0;
    private Data data;
    private int playerNumber;
    private int[] lastRollDiceResult = new int[2];
    private bool lastBoolDecision;

    public Player(Data data)
    {
        this.data = data;
        this.playerNumber = indexer;
        indexer++;
    }

    public int PlayerNumber { get => this.playerNumber; }
    public int[] LastRollDiceResult { get => this.lastRollDiceResult; }
    public void RollDice(Random random)
    {
        this.lastRollDiceResult = Dice.Roll(random);
        data.UpdatePlayer(this);
    }

    public bool LastBoolDecision
    {
        get 
        {
            return this.lastBoolDecision;
        }
        set
        {
            this.lastBoolDecision = value;
            data.UpdatePlayer(this);
        }
    }

    public void WantToUseJailFreeCard()
    {
        if (data.FreeJailCards[this.playerNumber] != 0)
        {
            this.LastBoolDecision = true;
        }
        else
        {
            this.LastBoolDecision = false;
        }
    }
}
